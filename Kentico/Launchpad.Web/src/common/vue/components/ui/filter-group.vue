<!-- Built with Common Launchpad 2.0.2 -->

<template>
	<div class="filter-group" v-if="filter.hideFilter === 'False'">
		<div v-if="filter.label" class="control-item control-item-header">
			<label v-bind:for="filter.label.replace(' ', '-')+'-filter'">{{ filter.label }}</label>
			<button class="control-expand-icon fas fa-angle-down" type="button" v-if="filter.collapsible" v-b-toggle="'collapse-' + filter.specification">
				<span class="sr-only">collapse</span>
			</button>
		</div>

		<button v-if="filter.collapsible" class="expand-all" v-on:click="expand">
			<span v-if="allExpanded">Collapse All</span>
			<span v-if="!allExpanded">Expand All</span>
		</button>

		<b-collapse v-bind:class="filter.className" v-if="filter.subgroup != undefined" v-bind:ref="'collapse-' + filter.specification" v-bind:id="'collapse-' + filter.specification" v-bind:visible="filter.expandDefault">
			<checkbox-control @updatecheckboxfilter="updateCheckboxFilter" v-bind:id="'collapse-' + filter.specification" v-if="filter.groupType === 'checkbox'" v-bind:controls="filter.subgroup" v-bind:parent="filter" v-bind:specification="filter.specification"></checkbox-control>
			<radio-control v-if="filter.groupType === 'radio'" v-bind:controls="filter.subgroup" v-bind:parent="filter" v-bind:specification="filter.specification"></radio-control>
			<select-control v-if="filter.groupType === 'select'" v-bind:control="filter.subgroup" v-bind:defaultValue="filter.defaultValue" v-bind:specification="filter.specification"></select-control>
			<textbox-control v-if="filter.groupType === 'textbox'" v-bind:control="filter.subgroup" v-bind:parent="filter" v-bind:className="filter.className"></textbox-control>
		</b-collapse>
	</div>
</template>

<script>
	import { mapState, mapActions, mapMutations } from 'vuex';

	export default {
		props: ['filter', 'index'],
		data: function () {
			return {
				allExpanded: true,
				selectedValues: []
			}
		},
		beforeCreate: function () {
			this.$options.components.CheckboxControl = require('./checkbox-control.vue').default;
			this.$options.components.SelectControl = require('./select-control.vue').default;
			this.$options.components.TextboxControl = require('./textbox-control.vue').default;
			this.$options.components.RadioControl = require('./radio-control.vue').default;
		},
		methods: {
			...mapMutations('listing', { updateSpecification: 'updateSpecification' }),
			expand() {
				this.allExpanded = !this.allExpanded;

				for (var i = 0; i < this.filter.subgroup.length; i++) {
					if (this.filter.subgroup[i].collapsible) {
						if (this.allExpanded) {
							this.$refs['collapse-' + this.filter.specification].$children[0].$refs['collapse-' + i][0].$data.show = true;
						}
						else {
							this.$refs['collapse-' + this.filter.specification].$children[0].$refs['collapse-' + i][0].$data.show = false;
						}
					}

					if (this.filter.subgroup[i].subgroup != undefined && this.filter.subgroup[i].subgroup.length > 0) {
						this.expandChildren(this.filter.subgroup[i].subgroup, this.$refs['collapse-' + this.filter.specification].$children[0].$refs['collapse-' + i][0]);
					}
				}
			},
			expandChildren(group, parentRef) {
				for (var i = 0; i < group.length; i++) {
					if (group[i].collapsible) {
						if (this.allExpanded) {
							parentRef.$children[0].$refs['collapse-' + i][0].$data.show = true;
						}
						else {
							parentRef.$children[0].$refs['collapse-' + i][0].$data.show = false;
						}
					}

					if (group[i].subgroup != undefined && group[i].subgroup.length > 0) {
						this.expandChildren(group[i].subgroup, parentRef.$children[0].$refs['collapse-' + i][0]);
					}
				}
			},
			updateCheckboxFilter() {
				this.selectedValues = this.$refs['collapse-' + this.filter.specification].$children[0].selectedValues;

				for (var i = 0; i < this.filter.subgroup.length; i++) {
					if (this.filter.subgroup[i].subgroup != undefined && this.filter.subgroup[i].subgroup.length > 0) {
						this.getCheckboxFilters(this.filter.subgroup[i].subgroup, this.$refs['collapse-' + this.filter.specification].$children[0].$refs['collapse-' + i][0]);
					}
				}

				this.updateSpecification({ key: this.filter.specification, value: this.selectedValues });
			},
			getCheckboxFilters(group, parentRef) {
				let values = parentRef.$children[0].selectedValues;
				this.selectedValues = [...this.selectedValues, ...values];

				for (var i = 0; i < group.length; i++) {
					if (group[i].subgroup != undefined && group[i].subgroup.length > 0) {
						this.getCheckboxFilters(group[i].subgroup, parentRef.$children[0].$refs['collapse-' + i][0]);
					}
				}
			},
		},
	}
</script>