/// <binding ProjectOpened='Watch - Development' />
const path = require('path');
const glob = require("glob");
const argv = require('yargs').argv;
const VueLoaderPlugin = require('vue-loader/lib/plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const UglifyJsPlugin = require('uglifyjs-webpack-plugin');
const CleanPlugin = require('clean-webpack-plugin');
const PostBuildDeletePlugin = require('webpack-delete-after-emit')
const BundleAnalyzerPlugin = require('webpack-bundle-analyzer').BundleAnalyzerPlugin;
const StyleLintPlugin = require('stylelint-webpack-plugin');
const ModernizrWebpackPlugin = require('modernizr-webpack-plugin');
const ImageminWebpackPlugin = require('imagemin-webpack-plugin').default;
const ImageminWebpWebpackPlugin = require("imagemin-webp-webpack-plugin");
const CopyWebpackPlugin = require('copy-webpack-plugin');

const isDevelopment = process.env.NODE_ENV === 'development';
const isProduction = !isDevelopment;

const contentPath = path.join(__dirname, '/src');
const outputPath = path.join(__dirname, '/dist');

const adminFormPath = path.join(__dirname, '/Content/FormSections/Shared');

var common = glob.sync(`${contentPath}/views/Common/*.js`);
var custom = glob.sync(`${contentPath}/views/Custom/*.js`);


function getJSViews(common, custom) {
    common.forEach(function (commonPath, i) {
        var commonFile = commonPath.substring(commonPath.lastIndexOf('/') + 1);

        custom.forEach(function (customPath, j) {
            var customFile = customPath.substring(customPath.lastIndexOf('/') + 1);

            if (commonFile == customFile) {
                common.splice(i, 1);
            }
        });
    });

    return common.concat(custom);
}


var entries = toObject(getJSViews(common, custom));

// common config settings
const config = {
    entry: entries,
    output: {
        filename: '[name].min.js',
        path: `${outputPath}`,
        chunkFilename: '[name].min.js',
        jsonpFunction: 'wpJsonpLaunchpad',
    },
    externals: {
        jquery: 'jQuery',
    },
    mode: isDevelopment ? 'development' : 'production',
    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /node_modules/,
                use: [
                    {
                        loader: "babel-loader",
                        options: {
                            presets: ['@babel/preset-env'],
                            "plugins": [
                                ['@babel/plugin-proposal-object-rest-spread'],
                                ["babel-plugin-root-import"]
                            ]
                        }
                    },
                    {
                        loader: "eslint-loader",
                    }
                ]
            },
            {
                test: /\.vue$/,
                use: [{
                    loader: 'vue-loader',
                    options: {
                        loaders: {
                            js: {
                                loader: 'babel-loader',
                                options: {
                                    presets: ['@babel/preset-env'],
                                    plugins: ['@babel/plugin-proposal-object-rest-spread']
                                }
                            },
                        }
                    }
                }],
            },
            {
                test: /\.scss$/,
                exclude: /node_modules/,
                use: [
                    MiniCssExtractPlugin.loader,
                    'css-loader',
                    {
                        loader: 'postcss-loader',
                        options: {
                            plugins: [
                                isProduction ? require('cssnano') : () => { },
                                require('autoprefixer')({
                                    grid: true,
                                    browsers: ['last 2 versions', 'ie 9', 'ios 6', 'android 4']
                                })
                            ]
                        }
                    },
                    'sass-loader'
                ]
            },
            {
                test: /\.css$/,
                use: ['vue-style-loader', 'css-loader']
            },
            {
                test: /\.(jpeg|jpg|png|gif|svg|woff|woff2|eot|ttf)$/,
                use: [{
                    loader: 'url-loader',
                    options: {
                        // encodes files < 32kB as base64 data urls
                        limit: 32000,
                        name: 'assets/[hash].[ext]'
                    }
                }]
            }
        ]
    },
    plugins: [
        new VueLoaderPlugin(),
        //new BundleAnalyzerPlugin(),
        new MiniCssExtractPlugin({
            filename: '[name].min.css'
        }),
        new StyleLintPlugin({
            configFile: '.stylelintrc',
            files: ['src/css/scss/**', 'src/scss/components/**', 'src/scss/layout/**'],
            ignorePath: '.stylelintignore'
        }),
        new ModernizrWebpackPlugin({
            filename: 'modernizr.min.js',
            minify: {
                output: {
                    comments: true,
                    beautify: true
                }
            },
            'feature-detects': [
                'img/webp'
            ]
        }),
        new CopyWebpackPlugin([{
            from: 'Content/images/',
            to: path.resolve(__dirname, 'Content/images/')
        }]),
        new ImageminWebpackPlugin(),
        new ImageminWebpWebpackPlugin({
            options: {
                quality: 75
            }
        }),
    ],
    optimization: {
        //    splitChunks: {
        //        cacheGroups: {
        //vue: {
        //	test: /[\\/]node_modules[\\/](vue|vee-validate|@vue|vuex-map-fields|vue-loader|vuex|vue-simple-spinner|vue-template-compiler|vue-fragment|bootstrap-vue)[\\/]/,
        //	name: 'vue-vendor',
        //	priority: 10,
        //	chunks: 'all',
        //},
        //vendors: {
        //	chunks: 'all',
        //	name: 'vendor',
        //	priority: -10,
        //	test: /[\\/]node_modules[\\/]/,
        //}
        //        }
        //    },
        minimizer: [
            new UglifyJsPlugin({
                sourceMap: true,
                parallel: true,
                sourceMap: true
            }),
        ]
    },
    devtool: 'source-map',
    resolve: {
        alias: {
            'vue$': 'vue/dist/vue.esm.js'
        },
        extensions: ['*', '.js', '.vue', '.json', '.scss']
    },
};

function toObject(paths) {
    var ret = {};

    paths.forEach(function (path) {
        // you can define entry names mapped to [name] here
        ret[path.replace('.js', '').split('/').slice(-1)[0]] = path;
    });

    return ret;
}

module.exports = config;