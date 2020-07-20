<template>
  <div>
    <validation-observer ref="observer" v-slot="{ handleSubmit }">
      <h2 style="margin:30px; text-align:center">Kreiranje kursa</h2>
      <b-form @submit.prevent="handleSubmit(handleCreateSubmit)" @reset.prevent="handleReset">
        <validation-provider
          name="Ime kursa"
          :rules="{ required: true, min: 3 }"
          v-slot="validationContext"
        >
          <b-form-group id="input-group-2" label="Naziv kursa:" label-for="input-1">
            <b-form-input
              id="input-1"
              v-model="formData.nazivKurs"
              required
              :state="getValidationState(validationContext)"
              placeholder="Unesite ime kursa"
            ></b-form-input>
            <b-form-invalid-feedback id="input-1-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
          </b-form-group>
        </validation-provider>

<validation-provider
          name="Ime kursa"
          :rules="{ required: true }"
          v-slot="validationContext"
        >
         <b-form-group label="Studenti: ">
      <b-form-checkbox
        v-for="option in options"
        v-model="selected"
        :key="option.StudentId"
        :value="option.StudentId"
        :state="getValidationState(validationContext)"
        name="flavour-3a"
      >
        {{ option.IndeksBroj }} 
      </b-form-checkbox>
      
    </b-form-group>
</validation-provider>

        <b-button type="submit" variant="primary">Kreiraj</b-button>
        <b-button type="reset" style="margin-left:2%" variant="primary">Resetuj</b-button>
      </b-form>
    </validation-observer>
    <br />
  </div>
</template>
<script>
import Vue from "vue";
import axios from "axios";
import VueAxios from "vue-axios";
import { mapState } from "vuex";
import basepath from "@/constants/basepath.js";

Vue.use(VueAxios, axios);

import {
  ValidationObserver,
  ValidationProvider,
  extend,
  localize
} from "vee-validate";
import en from "vee-validate/dist/locale/en.json";
import * as rules from "vee-validate/dist/rules";

Object.keys(rules).forEach(rule => {
  extend(rule, rules[rule]);
});

localize("en", en);

Vue.component("ValidationObserver", ValidationObserver);
Vue.component("ValidationProvider", ValidationProvider);

export default {
  props: {},
  data() {
    return {
      selected: [],
      formData: {
        nazivKurs: ''
      },
      options: [],
      listaStudenataId: []
    };
  },
  mounted() {
    this.options = this.allStudents
  },
   computed: {
    ...mapState(["accessTokenState"]),
    ...mapState(["allStudents"])
  },
  methods: {
    async handleCreateSubmit() {

     await this.selected.forEach(element => {
        this.listaStudenataId.push(element)
      });

       axios
        .post(
          basepath.baseUrl +
            "api/Kurs/AddKurs/" +
            this.formData.nazivKurs,
          this.listaStudenataId,
          {
            headers: {
              Authorization: "Bearer " + this.accessTokenState
            }
          }
        )
        .then(response => {
          console.log(response);
          alert("Uspješno ste kreirali kurs");

          this.handleReset();
        })
        .catch(e => console.log(e));
    },
    getValidationState({ dirty, validated, valid = null }) {
      return dirty || validated ? valid : null;
    },
    handleReset() {
      (this.formData.nazivKurs = ""),
        (this.selected = []);

      this.$refs.observer.reset()
    }
  }
};
</script>

