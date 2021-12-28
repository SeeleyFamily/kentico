<!-- Built with Common Launchpad 2.0.2 -->

<template>
    <div class="card summary-item col-12 col-lg-4">
        <a v-bind:href="item.Url" class="card-content-wrapper card-link-wrapper">
            <div class="card-body">
                <div class="card-media" role="img" v-bind:aria-label="item.Title" v-bind:style="{ 'background-image': 'url(' + image + ')' }"></div>
                <div class="card-body-content-wrapper">
                    <div class="card-tags" v-if="types.length">
                        <span class="card-tags-item" v-for="tag in types">{{ tag.Text }}</span>
                    </div>
                    <h2 class="card-title">{{ item.Title }}</h2>
                </div>
                <div class="card-cta">
                    <div class="link m-0" v-bind:class="item.Type.CodeName.toLowerCase()">{{ item.Cta.Text }}</div>
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
            types: function () {
                if(this.item.Tags){
				    return this.item.Tags.filter((item) => {
					    return item.Type.CodeName == 'Types';
				    }).slice(0, 2);
                }else{
                    return [];
                }
            },
            image: function () {
                let html = document.querySelector('html');

                if (html.classList.contains('webp')){
                    if (this.item.Image !== null && this.item.Image !== "") {
                        return imageOptimizer.getOptimizedImageUrl(this.item.Image, 'webp');
                    }
                    else {
                        return '/content/images/content-placeholder.webp';
                    }
                }
                else {
                    if (this.item.Image !== null && this.item.Image !== "") {
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
    }
</script>