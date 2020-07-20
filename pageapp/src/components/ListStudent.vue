<template>
  <div>
    <h1 style="text-align:center">Lista studenata</h1>
    <b-list-group v-if="students" style="margin:10%">
      <div>
        <b-list-group-item v-for="(student,index) in students" :key="student.StudentId">
          <div style="width:20%; display:inline-block; margin-left:15%; margin-right:5%">
            <b>{{index+1}}. {{student.IndeksBroj}}</b>
          </div>

          <b-button
            @click="pregledajStudent(student.StudentId)"
            variant="info"
            pill
            id="btnMargin"
          >Pregled</b-button>

          <b-button @click="editStudent(student.StudentId)" pill id="btnMargin">Izmjena</b-button>

          <b-button
            @click="obrisiStudent(student.StudentId)"
            variant="danger"
            pill
            id="btnMargin"
          >Brisanje</b-button>
        </b-list-group-item>
      </div>
    </b-list-group>
  </div>
</template>

<script>
import Vue from "vue";
import axios from "axios";
import VueAxios from "vue-axios";
import { mapState, mapActions } from "vuex";
import basepath from "@/constants/basepath.js";

Vue.use(VueAxios, axios);

export default {
  data() {
    return {
      students: []
    };
  },
  computed: {
    ...mapState(["accessTokenState"])
  },
  methods: {
    pregledajStudent(idp) {
      this.$router.push({ name: "studentdetails", params: { id: idp } });
    },
    editStudent(idp) {
      this.$router.push({ name: "editstudent", params: { id: idp } });
    },
    obrisiStudent(idp) {
      if (confirm("Da li ste sigurni da zelite obrisati?")) {
        axios
          .delete(basepath.baseUrl + "api/Student/DeleteStudent/" + idp, {
            headers: {
              Authorization: "Bearer " + this.accessTokenState
            }
          })
          .then(() => {
            this.students.splice(
              this.students.findIndex(x => x.StudentId === idp),
              1
            );
          })
          .catch(error => {
            console.log(error);
          });
      }
    },
    ...mapActions(["storeStudents"])
  },
  props: {},
  mounted() {
    axios
      .get(
        basepath.baseUrl + "api/Student/GetAllStudents",

        {
          headers: {
            Authorization: "Bearer " + this.accessTokenState
          }
        }
      )
      .then(response => {
        this.students = response.data;
        this.storeStudents(this.students);
      })
      .catch(e => console.log(e));
  }
};
</script>

<style scoped>
#btnMargin {
  padding: 10px;
  margin-left: 10px;
}
</style>