<!-- Built with Common Launchpad 2.0.2 -->

<template>
    <div v-bind:class="isPastDate(item.EndDate, item.StartDate) ? 'card--past' : 'card--upcoming'" class="card card--left-align summary-item col-12 col-lg-4">
        <div class="card-content-wrapper">
            <div class="card-body">
                <template v-if="item.Url">
                    <a v-bind:href="item.Url" class="card-body-content-wrapper card-link-wrapper" v-bind:target="item.IsExternal ? '_blank' : '_self'" v-on:click="gtmOnClick(item)">
                        <div class="card-date">{{ formatDate(item.StartDate, item.EndDate) }}</div>
                        <div class="card-tags" v-if="item.Tags.length">
                            <span class="card-tags-item" v-for="tag in typeTags.slice(0, 2)">{{ tag.Text }}</span>
                        </div>
                        <h2 class="card-title">{{ item.Title }}</h2>
                        <div class="card-cta link m-0" v-bind:class="{'card-cta--external': item.IsExternal}" v-if="item.Url">See Event</div>
                    </a>
                </template>
                <template v-else>
                    <a class="card-body-content-wrapper card-link-wrapper">
                        <div class="card-date">{{ formatDate(item.StartDate, item.EndDate) }}</div>
                        <div class="card-tags" v-if="item.Tags.length">
                            <span class="card-tags-item" v-for="tag in typeTags.slice(0, 2)">{{ tag.Text }}</span>
                        </div>
                        <h2 class="card-title">{{ item.Title }}</h2>
                    </a>
                </template>
            </div>
        </div>
    </div>
</template>

<script>
    import moment from 'moment';
    import analytics from '../../utils/analytics';

	export default {
        props: ['item'],
		components: {

        },
        computed: {
            typeTags: function () {
                return this.item.Tags.filter(function (item) {
                    return item.Type.CodeName == 'Types';
                })
            }
        },
		mounted: function () {

		},
        methods: {
            isPastDate: function (endDate, startDate) {
                let isPast = false;
                let date;
                const currentDate = moment().utc().format('MM/DD/YYYY');
                if (endDate !== null) {
                    date = moment(endDate).utc().format('MM/DD/YYYY');
                } else {
                    date = moment(startDate).utc().format('MM/DD/YYYY');
                }

                if (date === "Invalid date") {
                    isPast = true;
                }
                else if (date !== currentDate && moment(date).isBefore(currentDate) === true) {
                    isPast = true;
                }

                return isPast;
            },
            formatDate: function (startDate, endDate) {
                if (startDate !== null) {
                    startDate = moment.utc(startDate).format('MMM. DD, YYYY');
                }

                if (endDate !== null) {
                    endDate = moment.utc(endDate).format('MMM. DD, YYYY');
                }

                // endDate !== null && moment(startDate).isSame(endDate)
                if (endDate !== null && startDate === endDate) {
                    return startDate;
                }
                else {
                    if (endDate !== null) {
                        // moment(startDate).isSame(endDate, 'month')
                        if (startDate.substring(0,3) === endDate.substring(0,3)) {
                            return moment.utc(startDate, 'MMM. DD, YYYY').format('MMM. D') + '-' + moment.utc(endDate, 'MMM. DD, YYYY').format('D, YYYY');
                        }
                        else {
                            return moment(startDate, 'MMM. DD, YYYY').format('MMM. D') + '-' + moment(endDate, 'MMM. DD, YYYY').format('MMM. D, YYYY');
                        }
                    }
                    else {
                        return startDate;
                    }
                }
            },
            gtmOnClick: function (item) {
                let data = {
                    'full_event_name': `${item.Title}`,
                    'event_date': `${this.formatDate(item.StartDate, item.EndDate)}`,
                    'event': `${item.Title}`,
                    'location': `${item.Location}`,
                };

				analytics.gtmClickEvent('viewEventDetailsClick', data);
            }
        },
    }
</script>