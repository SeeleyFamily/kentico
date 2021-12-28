<!-- Built with Common Launchpad 2.0.2 -->

<template>
	<div class="control">
		<div class="control-item" v-bind:class="{ collapsible: radio.collapsible, 'has-children': (radio.c != undefined && radio.specification.length > 0) }" v-for="radio in radios">
			<div class="form-group custom-radio">
				<input type="radio" v-bind:value="radio.value" v-model="selectedValues" v-on:change="updateFilters" />
				<label>{{ radio.label }}</label>

				<button class="control-expand-icon fas fa-angle-down" type="button" v-if="radio.collapsible" v-b-toggle="'collapse-' + radio.value"><span class="sr-only">collapse</span></button>
			</div>

			<b-collapse v-if="radio.specification != undefined && radio.specification.length" v-bind:ref="'collapse-' + radio.value" v-bind:id="'collapse-' + radio.value" v-bind:visible="radio.expandDefault">
				<radio-control v-bind:controls="radio.specification" v-bind:parent="radio" v-bind:specification="radio.specification"></radio-control>
			</b-collapse>
		</div>
	</div>
</template>

<script>
	import { mapState, mapActions, mapMutations } from 'vuex';

	export default {
		props: ['controls', 'parent', 'specification'],
		data: function () {
			return {
				radios: this.controls,
				selectedValues: ''
			}
		},
		beforeCreate: function () {
			this.$options.components.RadioControl = require('./radio-control.vue').default;
		},
		mounted: function () {
			// clear current input on reset
			this.$store.subscribe((mutation, state) => {
				if (mutation.type === 'listing/resetSpecification') {
					this.selectedValues = '';
				}
			});
		},
		methods: {
			...mapMutations('listing', { updateSpecification: 'updateSpecification' }),
			updateFilters() {
				// if hitting api on each checkmark change
				this.updateSpecification({ key: this.specification, value: this.selectedValues });

			},
		}
	}
</script>