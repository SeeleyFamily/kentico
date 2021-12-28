<!-- Built with Common Launchpad 2.0.2 -->

<template>
	<div class="control">
		<div class="control-item" v-bind:class="{ collapsible: checkbox.collapsible, 'has-children': (checkbox.subgroup != undefined && checkbox.subgroup.length > 0) }" v-for="(checkbox, index) in checkboxes">
			<div class="form-group custom-checkbox">
				<input tabindex="0" type="checkbox" v-bind:id="checkbox.value+'-filter'" v-bind:value="checkbox.value" v-model="selectedValues" v-on:change="updateFilters(checkbox, $event)" v-bind="dataAttr(checkbox)" />
				<label tabindex="-1" v-bind:for="checkbox.value+'-filter'">{{ checkbox.label }}</label>

				<button class="control-expand-icon fas fa-angle-down" type="button" v-if="checkbox.collapsible" v-b-toggle="'collapse-' + index"><span class="sr-only">collapse</span></button>
			</div>

			<b-collapse v-if="checkbox.subgroup != undefined && checkbox.subgroup.length" v-bind:ref="'collapse-' + index" v-bind:id="'collapse-' + index" v-bind:visible="checkbox.expandDefault">
				<checkbox-control @updatecheckboxfilter="updateCheckboxFilter" v-bind:controls="checkbox.subgroup" v-bind:parent="checkbox" v-bind:specification="specification"></checkbox-control>
			</b-collapse>
		</div>
	</div>
</template>

<script>
	export default {
		props: ['controls', 'parent', 'specification'],
		data: function () {
			return {
				checkboxes: this.controls,
				selectedValues: []
			}
		},
		watch: {
			parent: {
				handler(newVal) {
					// remove logic if selecting parent should not select children
					//if (newVal.selected === true) {
					//	this.selectedValues = [];

					//	for (let check of this.checkboxes) {
					//		this.selectedValues.push(check.value);
					//		check.selected = true;
					//	}
					//}
				},
				deep: true,
			}
		},
		beforeCreate: function () {
			this.$options.components.CheckboxControl = require('./checkbox-control.vue').default;
		},
        mounted: function () {
            let initialData = this.$store.state.listing.specification[this.specification];

            if (initialData !== null && initialData !== '') {
                this.selectedValues = initialData;
            }
            else {
                this.selectedValues = ["999"];
            }

			// clear current input on reset
			this.$store.subscribe((mutation, state) => {
				if (mutation.type === 'listing/resetSpecification') {
					this.selectedValues = [];
				}
            });
        },
        updated: function () {
            if (this.selectedValues.length === 0) {
                this.selectedValues = [
                    "999"
                ];
            }
        },
		methods: {
            updateFilters(checkbox, $event) {
                // Check if value is 'All' (999)
                if ($event.target.value === '999') {
                    this.selectedValues = ["999"];
                }
                else {
                    // Remove previously selected 'All' (999)
                    this.selectedValues = this.selectedValues.filter(item => item !== '999');
                }

                this.$emit('updatecheckboxfilter');

				// if auto selecting all child checkboxes on parent select
				//if ($event.target.checked) {
				//	checkbox.selected = true;
				//}
				//else {
				//	checkbox.selected = false;
				//}
			},
			updateCheckboxFilter() {
				this.$emit('updatecheckboxfilter');
            },
            dataAttr(checkbox) {
                // Add GTM data attribute for recipe filters
                if (this.specification === "RecipeType") {
                    if (checkbox.value === "999") {
                        return { 'data-recipe-filter': checkbox.label };

                    }
                    else {
                        return { 'data-recipe-filter': checkbox.value };
                    }
                }
                else {
                    return {};
                }
            }
        }
	}
</script>