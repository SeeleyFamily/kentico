<!-- Built with Common Launchpad 2.0.2 -->

<template>
    <a v-bind:href="item.Cta.Url" class="card-link-wrapper">
        <div v-if="item.Image && item.Image != ''" class="card-img" v-bind:style="{ backgroundImage: 'url(' + item.Image + ')' }"></div>
        <div class="content-list-item">
            <div class="content-list-item-date">
                {{ item.Date | formatDate }} |
                <span class="content-list-item-authors">
                    {{ getAuthorList(item.Authors) }}
                </span>
            </div>
            <h2 class="content-list-item-title">
                {{ item.Title }}
            </h2>
            <div class="content-list-item-summary" v-html="decodeItem(item.Summary)">
            </div>
            <div v-if="item.Tags && item.Tags.length" class="content-list-item-tags">
                <span>Topics:</span>
                <ul>
                    <li v-for="tag in item.Tags" v-bind:key="tag.Text">
                        {{ tag.Text }}
                    </li>
                </ul>
            </div>
            <div class="content-list-item-cta">
                <div class="link">
                    {{ item.Cta.Text }}
                </div>
            </div>
        </div>
    </a>
</template>

<script>
    import moment from 'moment';
    import he from 'he';

	export default {
		props: ['item'],
		components: {

		},
		mounted: function () {
		},
        methods: {
            decodeItem(value) {
                return he.decode(value);
            },
            getAuthorList(authors) {
                var nameList = authors.map((v) => {
                    return v.Title;
                });
                return nameList.join(", ");
            }
        },
        filters: {
            formatDate: function(value) {
                if (value) {
                    return moment(value).format('MMMM DD, YYYY');
                }
            },
            decode: function (value) {
                return he.decode(value);
            }
        }
    }
</script>