<!-- Built with Common Launchpad 2.0.2 -->

<template>
    <div class="tag-cloud control">
        <label v-bind:for="label.replace(' ', '-')+'-filter'">{{ label }}</label>

        <ul class="tag-cloud-list">
            <li v-for="item in control">
                <div class="checkbox-container">
                    <input type="checkbox" id="checkbox" v-model="values" v-bind:value="item.value" v-on:change="selectTag">
                    <label for="checkbox">{{ item.label }}</label>
                </div>
            </li>
        </ul>

        <select id="Sort-By-filter" class="form-control bs-select" v-on:change="updateFilters" v-model="values" ref="select" multiple>
            <option v-for="item in control" v-bind:value="item.value" v-bind:key="item.value">{{ item.label }}</option>
        </select>
    </div>
</template>

<script>
    import 'bootstrap';
    import 'bootstrap-select';
    import { mapState, mapActions, mapMutations } from 'vuex';
    $.fn.selectpicker.Constructor.BootstrapVersion = '4';

	export default {
		props: ['control', 'defaultValue', 'specification', 'label'],
		data: function () {
			return {
                values: []
            }
        },
        computed: mapState({
            spec: state => state.listing.specification,
        }),
        mounted: function () {
            $('.bs-select').selectpicker({
                mobile: true,
                dropupAuto: false,
                size: 4,
            });

            let defaultSpec = this.spec[this.specification] !== null ? this.spec[this.specification] : this.defaultValue;
            this.values = defaultSpec !== null ? defaultSpec : [];

            this.$nextTick(function () {
                $('.bs-select').selectpicker('refresh');
            });

			// clear current input on reset
			this.$store.subscribe((mutation, state) => {
				if (mutation.type === 'listing/resetSpecification') {
                    this.values = [...this.defaultValue];

                    this.$nextTick(function () {
                        $('.bs-select').selectpicker('refresh');
                    });
				}
            });
        },
		methods: {
			...mapMutations('listing', { updateSpecification: 'updateSpecification' }),
			updateFilters() {
				this.updateSpecification({ key: this.specification, value: this.values });
            },
            selectTag() {
                this.updateSpecification({ key: this.specification, value: this.values });

                this.$nextTick(function () {
                    $('.bs-select').selectpicker('refresh');
                });
            }
		}
	}
</script>