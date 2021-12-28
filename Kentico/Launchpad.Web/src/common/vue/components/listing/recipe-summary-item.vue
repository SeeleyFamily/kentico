<!-- Built with Common Launchpad 2.0.2 -->

<template>
    <div class="card--recipe card summary-item col-12 col-lg-4">
        <a v-bind:href="item.Url" class="card-content-wrapper card-link-wrapper">
            <div class="card-body">
                <div class="card-media" role="img" v-bind:aria-label="item.Title" v-bind:style="{ backgroundImage: 'url(' + image + ')'}"></div>
                <div class="card-body-content-wrapper">
                    <div class="card-tags" v-if="item.Tags.length">
                        <span class="card-tags-item" v-for="tag in typeTags.slice(0, 2)">{{ tag.Text }}</span>
                    </div>
                    <h2 class="card-title">{{ item.Title }}</h2>
                </div>
                <div class="card-cta">
                    <div class="link m-0">{{ item.Cta.Text }}</div>
                </div>
            </div>
        </a>
    </div>
</template>

<script>
    import imageOptimizer from '../../utils/image-optimizer';

	export default {
		props: ['item'],
		components: {

        },
        computed: {
            typeTags: function () {
                return this.item.Tags.filter(function (item) {
                    return item.Type.CodeName == 'Types';
                })
            },
            image: function () {
                let html = document.querySelector('html');

                if (html.classList.contains('webp')){
                    if ($(window).width() < 768 && this.item.ImageMobile !== null && this.item.ImageMobile !== undefined && this.item.ImageMobile !== "") {
                        return imageOptimizer.getOptimizedImageUrl(this.item.ImageMobile, 'webp');
                    }
                    else if (this.item.Image !== null && this.item.Image !== "") {
                        return imageOptimizer.getOptimizedImageUrl(this.item.Image, 'webp');
                    }
                    else {
                        return '/content/images/content-placeholder.webp';
                    }
                }
                else {
                    if ($(window).width() < 768 && this.item.ImageMobile !== null && this.item.ImageMobile !== undefined && this.item.ImageMobile !== "") {
                        return imageOptimizer.getSanitizeMediaUrl(this.item.ImageMobile);
                    }
                    else if (this.item.Image !== null && this.item.Image !== "") {
                        return imageOptimizer.getSanitizeMediaUrl(this.item.Image);
                    }
                    else {
                        return '/content/images/content-placeholder.png';
                    }
                }
            }
        },
		mounted: function () {

		},
        methods: {
        },
        filters: {
            checkImage: function (value) {
                if (value !== null && value !== "") {
                    return value.replace("~", "");
                }
                else {
                    return "/Content/images/recipe-placeholder.png";
                }
            }
        }
    }
</script>