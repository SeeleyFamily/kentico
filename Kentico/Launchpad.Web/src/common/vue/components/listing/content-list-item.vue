<!-- Built with Common Launchpad 2.0.2 -->

<template>
    <a v-bind:href="item.Cta.Url" class="card-link-wrapper">
        <div class="content-list-item">
            <h2 class="content-list-item-title">{{ item.Title }}</h2>
            <div class="content-list-item-date">{{ item.Date | formatDate }}</div>
            <div class="content-list-item-summary">{{ item.Summary | decode }}</div>
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