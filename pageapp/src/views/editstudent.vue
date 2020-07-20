<template>
  <div>
    <div class="column clearfix">
      <EditStudentForm />
      <button
        style="margin-top:20px; float:right; "
        @click="toggleCreateComponent"
        class="btn btn-primary"
      >Dodaj novog studenta</button>
    </div>
    <div>
      <CreateStudentForm v-show="this.toggleCreate" />
    </div>
  </div>
</template>

<script>
import EditStudentForm from "@/components/EditStudentForm.vue";
import CreateStudentForm from "@/components/CreateStudentForm.vue";
export default {
  props: {},
  components: {
    EditStudentForm,
    CreateStudentForm
  },
  data() {
    return {
      toggleCreate: false
    };
  },
  methods: {
    toggleCreateComponent() {
      this.toggleCreate = !this.toggleCreate;
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