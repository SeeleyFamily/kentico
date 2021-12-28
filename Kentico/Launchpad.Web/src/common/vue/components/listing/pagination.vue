<!-- Built with Common Launchpad 2.0.2 -->

<template>
	<nav v-if="pagination.totalPages > 1" class="pagination" role="navigation" aria-label="pagination">
		<ul class="pagination-list">
			<li class="pagination-prev">
				<button class="pagination-previous" @click.prevent="changePage(pagination.pageIndex - 1)" v-bind:disabled="pagination.pageIndex <= 1 ? 'disabled' : false">Previous</button>
			</li>
			<li v-for="page in pages" class="pagination-page" taxindex="0">
				<button class="pagination-link" v-bind:class="isCurrentPage(page) ? 'active' : ''" @click.prevent="changePage(page)">{{ page }}</button>
			</li>
			<li class="pagination-next">
				<button class="pagination-next" @click.prevent="changePage(pagination.pageIndex + 1)" v-bind:disabled="pagination.pageIndex >= pagination.totalPages ? 'disabled' : false">Next</button>
			</li>
		</ul>
	</nav>
</template>

<script>
	import { mapState, mapActions, mapMutations } from 'vuex';

	export default {
		data: function () {
			return {
				pages: [],
				startPage: 0,
				endPage: 0,
			}
		},
		computed: mapState({
			pagination: state => state.listing.pagination,
			specification: state => state.listing.specification,
		}),
		watch: {
			pagination: {
				handler: function (newProp, oldProp) {
					// after api call, regenerate pages
					this.createStartEndPages();
				},
				deep: true,
				immediate: true
			},
		},
		mounted() {
			this.createStartEndPages();
		},
		methods: {
			...mapMutations('listing', { updateSpecification: 'updateSpecification' }),
			createStartEndPages() {
				this.startPage = this.pagination.pageIndex - 2;
				this.endPage = this.pagination.pageIndex + 2;

				if (this.startPage <= 0) {
					this.endPage -= this.startPage - 1;
					this.startPage = 1;
				}

				if (this.endPage > this.pagination.totalPages) {
					this.endPage = this.pagination.totalPages;

					if (this.endPage > 5) {
						this.startPage = this.endPage - 4;
					}
				}

				this.setPages();
			},
            isCurrentPage(page) {
                return this.pagination.pageIndex === page;
            },
            changePage(page) {
				if (page > this.pagination.totalPages) {
					page = this.pagination.totalPages;
				}

				this.updateSpecification({ key: 'PageIndex', value: page - 1 });
			},
			setPages() {
				let from = this.startPage;
				let to = this.endPage;
				this.pages = [];

				while (from <= to) {
					this.pages.push(from);
					from++;
				}
			}
        }
    }
</script>