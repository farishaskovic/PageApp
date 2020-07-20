<template>
  <div id="login">
    <h1>Login</h1>
    <input
      type="text"
      name="username"
      v-model="input.username"
      v-on:keyup.enter="login()"
      placeholder="Username"
    />
    <input
      type="password"
      name="password"
      v-model="input.password"
      v-on:keyup.enter="login()"
      placeholder="Password"
    />
    <button type="button" v-on:click="login()">Login</button>
  </div>
</template>

<script>
import Vue from "vue";
import axios from "axios";
import VueAxios from "vue-axios";
import basepath from "@/constants/basepath.js";
import { mapActions } from "vuex";

Vue.use(VueAxios, axios);

export default {
  name: "Login",
  data() {
    return {
      input: {
        username: "",
        password: ""
      },
      token: null
    };
  },
  methods: {
    async login() {
      if (this.input.username != "" && this.input.password != "") {
        let body =
          "grant_type=password&username=" +
          this.input.username +
          "&password=" +
          this.input.password;

        await axios
          .post(basepath.baseUrl + "token", body, {
            headers: {
              "Content-Type": "text/plain"
            }
          })
          .then(response => {
            //this.setState({data:response.data});
            this.token = response.data.access_token;
            console.log("evo token:::" + this.token);

            this.storeToken(this.token);
          })
          .catch(e => console.log(e));

        if (this.token !== null) {
          this.$emit("authenticated", true);
          this.$router.replace({ name: "secure" });
        } else {
          alert("Korisnicko ime ili Password je netacan");
        }
      } else {
        alert("Morate unijeti korisnicko ime i password");
      }
    },

    ...mapActions(["storeToken"])
  }
};
</script>

<style scoped>
#login {
  width: 500px;
  border: 1px solid #cccccc;
  background-color: #ffffff;
  margin: auto;
  margin-top: 200px;
  padding: 20px;
}
</style>