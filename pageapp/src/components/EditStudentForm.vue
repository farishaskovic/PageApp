<template>
  <div>
    <validation-observer ref="observer" v-slot="{ handleSubmit }">
      <h2 style="margin:30px; text-align:center">Izmjena studenta</h2>
      <b-form @submit.prevent="handleSubmit(handleEditSubmit)">
        <validation-provider
          name="Indeks"
          :rules="{ required: true, min: 6 }"
          v-slot="validationContext"
        >
          <b-form-group id="input-group-2" label="Broj Indeksa:" label-for="input-1">
            <b-form-input
              id="input-1"
              v-model="formData.indeksBroj"
              required
              :state="getValidationState(validationContext)"
              placeholder="Unesite broj indeksa"
            ></b-form-input>
            <b-form-invalid-feedback id="input-1-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
          </b-form-group>
        </validation-provider>

        <b-form-group id="input-group-2" label="Ime:" label-for="input-2">
          <b-form-input id="input-2" v-model="formData.ime" placeholder="Unesite ime"></b-form-input>
        </b-form-group>

        <b-form-group id="input-group-2" label="Prezime:" label-for="input-3">
          <b-form-input id="input-3" v-model="formData.prezime" placeholder="Unesite prezime"></b-form-input>
        </b-form-group>

        <validation-provider name="Status" :rules="{ required: true }" v-slot="validationContext">
          <b-form-group id="input-group-2" label="Status studenta:" label-for="input-4">
            <b-form-select
              id="input-4"
              v-model="formData.studentstatus"
              :options="statusOptions"
              :state="getValidationState(validationContext)"
              required
            ></b-form-select>
            <b-form-invalid-feedback id="input-4-live-feedback">{{ validationContext.errors[0] }}</b-form-invalid-feedback>
          </b-form-group>
        </validation-provider>

        <b-form-group id="input-group-2" label="Godina:" label-for="input-5">
          <b-form-input
            id="input-5"
            type="number"
            v-model="formData.godina"
            placeholder="Unesite godinu"
          ></b-form-input>
        </b-form-group>

        <b-button type="submit" variant="primary">Sacuvaj</b-button>
      </b-form>
    </validation-observer>
  </div>
</template>
<script>
import Vue from "vue";
import axios from "axios";
import VueAxios from "vue-axios";
import { mapGetters, mapState } from "vuex";
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
  computed: {
    ...mapGetters(["getStudentById"]),
    ...mapState(["accessTokenState"])
  },
  data() {
    return {
      student: [],

      formData: {
        indeksBroj: "",
        ime: "",
        prezime: "",
        godina: "",
        studentstatus: ""
      },
      statusOptions: []
    };
  },
  mounted() {
    this.student = this.getStudentById(this.$route.params.id);

    this.formData.indeksBroj = this.student.IndeksBroj;
    this.formData.ime = this.student.Ime;
    this.formData.prezime = this.student.Prezime;
    this.formData.godina = this.student.Godina;
    this.formData.studentstatus = this.student.StatusNaziv;

    axios
      .get(basepath.baseUrl + "api/Student/GetAllStudentStatus", {
        headers: {
          Authorization: "Bearer " + this.accessTokenState
        }
      })
      .then(response => {
        response.data.forEach(element => {
          this.statusOptions.push(element.StatusNaziv);
        });

        console.log(this.statusOptions);
      })
      .catch(e => console.log(e));
  },
  methods: {
    handleEditSubmit() {
      let ime, prezime, godina;
      if (this.formData.ime === "") {
        ime = "null";
      } else {
        ime = this.formData.ime;
      }
      if (this.formData.prezime === "") {
        prezime = "null";
      } else {
        prezime = this.formData.prezime;
      }
      if (this.formData.godina === "") {
        godina = "null";
      } else {
        godina = this.formData.godina;
      }

      axios
        .patch(
          basepath.baseUrl +
            "api/Student/UpdateStudent/" +
            this.student.StudentId +
            "/" +
            this.formData.indeksBroj +
            "/" +
            this.formData.studentstatus +
            "/" +
            ime +
            "/" +
            prezime +
            "/" +
            godina,
          {},
          {
            headers: {
              Authorization: "Bearer " + this.accessTokenState
            }
          }
        )
        .then(response => {
          console.log(response);
          alert("Uspješno ste izmijenili studenta");

          this.$router.go(-1);
        })
        .catch(e => console.log(e));
    },
    getValidationState({ dirty, validated, valid = null }) {
      return dirty || validated ? valid : null;
    }
  }
};
</script>

