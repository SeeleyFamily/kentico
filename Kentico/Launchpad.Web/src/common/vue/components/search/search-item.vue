<!-- Built with Common Launchpad 2.0.2 -->

<template>
	<div class="card summary-item">
		<a v-bind:href="item.Url" class="card-content-wrapper card-link-wrapper">
			<div class="card-body">
				<div class="card-body-content-wrapper">
					<div class="card-type" v-if="item.Type !== null">{{ item.Type.DisplayName }}</div>
					<h2 class="card-title">{{ item.Title }}</h2>

					<div v-if="item.Breadcrumbs !== null && item.Breadcrumbs.length">
						<ul>
							<li v-for="crumb in item.Breadcrumbs">
								<a :href='crumb.Url'>{{ crumb.Title }}</a>
							</li>
						</ul>
					</div>

					<div v-html="item.Summary"></div>
				</div>
				<div class="card-cta">
					<div class="link m-0" v-bind:class="$options.filters.className(item.Type)" v-if="item.Cta !== null">{{ item.Cta.Text }}</div>
				</div>
			</div>
		</a>
	</div>
</template>

<script>
	export default {
		props: ['item'],
		components: {

		},
		mounted: function () {

		},
		methods: {
        },
		filters: {
			className: function (type) {
				let className = '';

				if (type !== null) {
					className = type.CodeName.toLowerCase();
				}

				return className;
			}
		}
    }
</script>