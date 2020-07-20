<template>
  <div>
    <h1 style="text-align:center">Detalji Studenta</h1>

    <b-list-group v-if="student" style="margin:50px; font-size:20px">
      <b-list-group-item>
        <b>Ime: {{student.Ime}}</b>
      </b-list-group-item>
      <b-list-group-item>
        <b>Prezime: {{student.Prezime}}</b>
      </b-list-group-item>
      <b-list-group-item>
        <b>Broj Indeksa: {{student.IndeksBroj}}</b>
      </b-list-group-item>
      <b-list-group-item>
        <b>Godina: {{student.Godina}}</b>
      </b-list-group-item>
      <b-list-group-item>
        <b>Status: {{student.StatusNaziv}}</b>
      </b-list-group-item>

    </b-list-group>
    <b-list-group  v-if="kursevi.length" style="margin:50px; font-size:20px">
        <h1>Kursevi</h1>
      <b-list-group-item v-for="kurs in kursevi" :key="kurs.KursId" @click="pregledajKurs(kurs.KursId)">
        <b>{{kurs.KursNaziv}}</b>
      </b-list-group-item>
    </b-list-group>
  </div>
</template>

<script>
import Vue from "vue";
import axios from "axios";
import VueAxios from "vue-axios";
import { mapGetters, mapState } from "vuex";
import basepath from '@/constants/basepath.js'
Vue.use(VueAxios, axios);

export default {
  props: {},
  data() {
    return {
      student: {},
      kursevi:{}
    };
  },
  mounted() {
    this.student = this.getStudentById(this.$route.params.id);
    console.log("details student:" + this.student);

    axios
      .get(
        basepath.baseUrl + "api/Student/GetAllKurs/"+this.$route.params.id,

        {
          headers: {
            Authorization: "Bearer " + this.accessTokenState
          }
        }
      )
      .then(response => {
        this.kursevi = response.data;
      })
      .catch(e => console.log(e));
  },
  computed: {
    ...mapGetters(["getStudentById"]),
    ...mapState(["accessTokenState"])
  },
  methods: {
      pregledajKurs(idp){
      this.$router.push({ name: "kursdetails", params: { id: idp } });
      }
  }
};
</script>