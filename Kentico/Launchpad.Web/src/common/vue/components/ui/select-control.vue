<!-- Built with Common Launchpad 2.0.2 -->

<template>
	<div class="control">
		<div class="form-group">
			<select id="Sort-By-filter" class="form-control" v-on:change="updateFilters" v-model="value" ref="select">
				<option v-if="!defaultExists" v-bind:value="defaultValue">{{ defaultValue }}</option>
				<option v-for="value in control" v-bind:value="value.Value" v-bind:key="value.Value">{{ value.Name }}</option>
			</select>
		</div>
	</div>
</template>

<script>
    import 'bootstrap';
    import 'bootstrap-select';
    import { mapState, mapActions, mapMutations } from 'vuex';

	export default {
		props: ['control', 'defaultValue', 'specification'],
		data: function () {
			return {
				value: this.defaultValue,
            }
		},
		computed: {
			...mapState({
				spec: state => state.listing.specification,
			}),
			defaultExists: function () {
				if (this.control.filter(item => item.Value === this.defaultValue).length > 0) {
					return true;
				}
				else {
					return false;
				}
			}
		},
		watch: {
			spec: {
				handler(value) {
					if (value[this.specification] !== '' && value[this.specification] !== null) {
						this.value = value[this.specification];
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
                    this.value = this.defaultValue;
				}
            });
        },
		methods: {
			...mapMutations('listing', { updateSpecification: 'updateSpecification' }),
			updateFilters() {
				if (this.value !== this.defaultValue) {
					this.updateSpecification({ key: this.specification, value: this.value });
				}
			},
		}
	}
</script>