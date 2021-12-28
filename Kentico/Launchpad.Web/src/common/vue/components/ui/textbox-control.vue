<!-- Built with Common Launchpad 2.0.2 -->

<template>
	<div class="control">
		<div class="form-group">
			<input id="app-search-input" class="form-control" v-bind:class="className" type="text" v-model="query" v-bind:placeholder="control.placeholder" v-on:keyup.enter="updateFilters" />
			<label class="sr-only" for="app-search-input">Search</label>

			<button type="button" v-if="query" title="Clear" v-on:click="clearSearchTerm()" class="fas fa-times-circle search-clear"></button>
			<button type="button" v-if="control.searchButton"
					v-bind:disabled="query == undefined || query.length == 0"
					v-on:click="updateFilters">
				<span class="sr-only">search</span><i class="fas fa-search"></i>
			</button>
		</div>
	</div>
</template>

<script>
    import he from 'he';
	import { mapState, mapActions, mapMutations } from 'vuex';

	export default {
		props: ['control', 'className', 'parent'],
		data: function () {
			return {
				query: he.decode(this.parent.defaultValue),
            }
		},
		computed: {
			...mapState({
				spec: state => state.listing.specification,
			}),
		},
		watch: {
			spec: {
				handler(value) {
					if (value[this.control.specification] !== '' && value[this.control.specification] !== null) {
						this.query = value[this.control.specification];
					}
				},
				immediate: true,
				deep: true,
			}
		},
        mounted: function () {
			// clear current input on reset
			this.$store.subscribe((mutation, state) => {
				if (mutation.type === 'listing/resetSpecification') {
					this.query = this.control.defaultValue;
				}
            });
		},
		methods: {
            ...mapMutations('listing', { updateSpecification: 'updateSpecification' }),
			clearSearchTerm() {

				if (this.query !== '') {
					this.query = '';
					this.updateFilters();
				}
			},
            updateFilters() {
				this.updateSpecification({ key: this.control.specification, value: he.decode(this.query) });
			},
		}
	}
</script>