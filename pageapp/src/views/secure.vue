<template>
  <div id="secure">
    <ListStudent props />
  </div>
</template>

<script>
import ListStudent from "@/components/ListStudent.vue";
export default {
  name: "Secure",
  components: {
    ListStudent
  },
  data() {
    return {};
  }
};
</script>

<style scoped>
#secure {
  background-color: #ffffff;
  border: 1px solid #cccccc;
  padding: 20px;
  margin-top: 10px;
}
</style>