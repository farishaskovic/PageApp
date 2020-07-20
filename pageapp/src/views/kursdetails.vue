<template>
  <div v-if="ready">

      <div v-if="students.length">
      <h1 style="text-align:center">Studenti na kursu</h1>
<b-list-group  style="margin:10%; font-size:20px">
      <div>
        <b-list-group-item  v-for="(student,index) in students" :key="student.StudentId" @click="pregledajStudent(student.StudentId)">
          <div style="width:50%; display:inline-block; margin-left:15%; margin-right:5%">
            <b>{{index+1}}. {{student.Ime}} {{student.Prezime}}</b>
          </div>
        </b-list-group-item>
      </div>
</b-list-group>
      </div>

      <div v-else>
<h1  class="center">Nema studenata na kursu</h1>
      </div>

  </div>
</template>

<script>
import Vue from "vue";
import axios from "axios";
import VueAxios from "vue-axios";
import { mapState } from "vuex";
import basepath from "@/constants/basepath.js";

Vue.use(VueAxios, axios);

export default {
   
  props: {},
  components: {
    
  },
  data() {
    return {
        students:{},
        ready:false
    }
  },
  
  computed:{
    ...mapState(["accessTokenState"])
  },
  async mounted(){
      await axios
      .get(
        basepath.baseUrl + "api/Kurs/GetKursStudents/"+this.$route.params.id,

        {
          headers: {
            Authorization: "Bearer " + this.accessTokenState
          }
        }
      )
      .then(response => {
        this.students = response.data;
        this.ready=true
      })
      .catch(e => console.log(e));
  },
  methods:{
      pregledajStudent(idp){
          this.$router.push({ name: "studentdetails", params: { id: idp } });
      }
  }
  
};
</script>
<style scoped>
.center {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 400px;
}
</style>