<!-- Built with Common Launchpad 2.0.2 -->

<template>
    <nav v-if="pagination.totalPages > 1" class="pagination" role="navigation" aria-label="pagination">
        <ul class="pagination-list">
            <li class="pagination-prev">
                <button class="pagination-previous" @click.prevent="changePage(pagination.pageIndex - 1)" v-bind:disabled="pagination.pageIndex <= 1 ? 'disabled' : false"><span class="sr-only">previous</span><i class="fas fa-chevron-left"></i></button>
            </li>
            <li class="pagination-page">
                <button class="pagination-dropdown-label" @keyup.enter="togglePaginationDropdown($event)" @click.stop="togglePaginationDropdown($event)">Showing <span class="pagination-dropdown-pages">{{ pagination.pageIndex }} of {{ pages.length }}</span> <i class="fas fa-chevron-down"></i></button>
                <div class="pagination-dropdown">
                    <ul id="simplebar">
                        <li tabindex="0" class="pagination-dropdown-item" v-bind:data-page="page" v-bind:class="isCurrentPage(page) ? 'active' : ''" v-on:click="changePage(page)" v-on:keyup.enter="changePage(page)" v-for="page in pages">Page {{ page }}</li>
                    </ul>
                </div>
            </li>
            <li class="pagination-next">
                <button class="pagination-next" @click.prevent="changePage(pagination.pageIndex + 1)" v-bind:disabled="pagination.pageIndex >= pagination.totalPages ? 'disabled' : false"><span class="sr-only">next</span><i class="fas fa-chevron-right"></i></button>
            </li>
        </ul>
    </nav>
</template>

<script>
    import { mapState, mapActions, mapMutations } from 'vuex';
    import 'bootstrap-select';
    import SimpleBar from 'simplebar';

    export default {
        data: function () {
            return {
                pages: [],
                startPage: 0,
                endPage: 0,
                isPaginationOpen: false,
                isKeyboardPaginationOpen: false,
                simpleBar: '',
                enterCount: -1,
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

            $(document).on('click', (event) => {
                this.togglePaginationDropdown(event);
            });

            $('.pagination-dropdown-label').on('keyup', (event) => {
                this.togglePaginationDropdown(event);
            });

            // More than 4 pages, apply custom scrollbar
            if (this.pagination.totalPages > 4) {
                this.simpleBar = new SimpleBar(document.getElementById('simplebar'));
            }
        },
        methods: {
            ...mapMutations('listing', { updateSpecification: 'updateSpecification' }),
            createStartEndPages() {
                this.startPage = 1;
				this.endPage = this.pagination.totalPages;

				if (this.startPage <= 0) {
					this.startPage = 1;
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

                $('.pagination-dropdown').removeClass('active');
                $('.pagination-dropdown-label i').removeClass('fa-chevron-up').removeClass('fa-chevron-down').addClass('fa-chevron-down');
                this.enterCount = -1;
            },
            setPages() {
                let from = this.startPage;
                let to = this.endPage;
                this.pages = [];

                while (from <= to) {
                    this.pages.push(from);
                    from++;
                }
            },
            togglePaginationDropdown($event) {
                const $target = $($event.currentTarget);

                if (this.isPaginationOpen === false && $event.type !== 'keyup') {
                    $target.children('i').removeClass('fa-chevron-down').addClass('fa-chevron-up');
                    $target.siblings('.pagination-dropdown').addClass('active');
                    this.isPaginationOpen = true;

                    if (this.simpleBar !== '') {
                        let scroll = this.simpleBar.getScrollElement();
                        let top = 0;

                        $(scroll).scrollTop(0);

                        // More than 4 pages, apply custom scrollbar
                        if (this.pagination.totalPages > 4) {
                            this.simpleBar = new SimpleBar(document.getElementById('simplebar'));
                        }

                        if ($('.pagination-dropdown-item.active').length){
                            top = $('.pagination-dropdown-item.active').position().top;
                        }

                        if (top !== 0) {
                            $(scroll).scrollTop(top);
                        }
                    }
                }
                else {
                    $target.children('i').removeClass('fa-chevron-up').addClass('fa-chevron-down');
                    $target.siblings('.pagination-dropdown').removeClass('active');
                    this.isPaginationOpen = false;
                }

                // Close if clicking anywhere outside of pagination area
                if ($target.parentsUntil('pagination').length === 0) {
                    $('.pagination-dropdown-label').children('i').removeClass('fa-chevron-up').addClass('fa-chevron-down');
                    $('.pagination-dropdown').removeClass('active');
                    this.isPaginationOpen = false;
                }

                // Keyboard focus functionality
                if ($event.type === 'keyup' && $event.key === "Enter") {
                    // Enter key fires twice, so we have to do some odd handling of that
                    this.enterCount++;

                    if (this.enterCount <= 1) {
                        $target.children('i').removeClass('fa-chevron-down').addClass('fa-chevron-up');
                        $target.siblings('.pagination-dropdown').addClass('active');
                        this.isKeyboardPaginationOpen = true;

                        if (this.simpleBar !== '') {
                            let scroll = this.simpleBar.getScrollElement();
                            $(scroll).scrollTop(0);

                            let top = $('.pagination-dropdown-item.active').position().top;

                            if (top !== 0) {
                                $(scroll).scrollTop(top);
                            }
                        }
                    }
                    else {
                        if (this.enterCount <= 999) {
                            $target.children('i').removeClass('fa-chevron-up').addClass('fa-chevron-down');
                            $target.siblings('.pagination-dropdown').removeClass('active');
                            this.isKeyboardPaginationOpen = false;
                            this.enterCount = 999;
                        }
                        else {
                            $target.children('i').removeClass('fa-chevron-up').addClass('fa-chevron-down');
                            $target.siblings('.pagination-dropdown').removeClass('active');
                            this.isKeyboardPaginationOpen = false;
                            this.enterCount = -1;
                        }
                    }
                }
            }
        }
    }
</script>