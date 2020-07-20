<template>
  <div>
      <h1 style="text-align:center">Lista kurseva</h1>
      <b-list-group v-if="kursevi" style="margin:10%; font-size:20px" >
      <div>
        <b-list-group-item   v-for="(kurs,index) in kursevi" :key="kurs.KursId" @click="pregledajKurs(kurs.KursId)">
            <b>{{index+1}}. {{kurs.KursNaziv}}</b>
        </b-list-group-item>
          </div>
      </b-list-group>
      <div class="column clearfix">
      <button
        style="margin-top:20px; float:right; "
        @click="toggleCreateComponent"
        class="btn btn-primary"
      >Dodaj novi kurs</button>

 <CreateKurs v-if="toggleKurs"/>
      </div>
  </div>
</template>

<script>
import Vue from "vue";
import axios from "axios";
import VueAxios from "vue-axios";
import basepath from "@/constants/basepath.js";
import { mapState } from "vuex";

import CreateKurs from "@/components/CreateKurs.vue"

Vue.use(VueAxios, axios);
export default {
  props: {},
  components: {
    CreateKurs
  },
  data() {
    return {
        kursevi: {},
        toggleKurs: false
    };
  },
  mounted(){
       axios
      .get(
        basepath.baseUrl + "api/Kurs/GetAllKurs",
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
  computed:{
    ...mapState(["accessTokenState"])
  },
  methods: {
      pregledajKurs(idp){
      this.$router.push({ name: "kursdetails", params: { id: idp } });
      },
      toggleCreateComponent(){
          this.toggleKurs = !this.toggleKurs
      }
  }
};
</script>
<style scoped>
.clearfix:before,
.clearfix:after {
  content: "\0020";
  display: block;
  height: 0;
  overflow: hidden;
}
.clearfix:after {
  clear: both;
}
.clearfix {
  zoom: 1;
}
</style>