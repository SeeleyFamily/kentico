# Introduction 
This repo contains the source code for Launchpad

# Versions
node -v
v14.16.1

npm -v
6.14.12

# Getting Started
1. Please refer to the README files within the `/src/js` and `/src/scss` folders for 
    general front-end standards to follow related to JavaScript and SASS respectively.

# Build and Test
1.	Before you can build the front-end assets, you must first install Node **>v6.0**. You can check the version of node you have installed by with the following command:
    $ node --version

2.	To auto build the front-end assests in Visual Studio, you can install [WebPack Task Runner Extension](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.WebPackTaskRunner). 
3.  On start-up, Visual Studio will automatically restore pacakges (`npm install`), and start webpack in development watch mode. To view the output, open the Task Runner Explorer by navigating to `Tools > Task Runner Explorer`.

3.	If you prefer to use command line, you can run the following commands with `npm run`:

* `build`: Compiles source assets in `development` mode.

* `build-release`: Compiles sources in `production` mode, minimized.

* `watch`: Runs webpack in `watch` mode, which recompiles source in `development` mode.


# Troubleshooting
If webpack is erroring, not due to code changes, you can try the following to 'reset' webpack:
* Delete the `node_modules` folder
* Delete the `package-lock.json` file
* Re-run `npm install`

If you're having issues compiling due to an error with `sass-loader`,or `node-sass`, you can run try running `npm rebuild node-sass`.
If it absolutley fails, then please add the node js folder path to top in Web external tools (Visual Studio tools -> options -> project and solution -> Web package management -> External Web Tools, then add node js path to top (C:\Program Files\nodejs)
Refernce link: https://stackoverflow.com/questions/40846006/vs-task-runner-explorer-node-sass-could-not-find-a-binding
