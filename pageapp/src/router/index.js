import Vue from 'vue'
import VueRouter from 'vue-router'
import LoginComponent from "../views/login.vue"
import SecureComponent from "../views/secure.vue"
import studentdetailsComponent from '../views/studentdetails.vue'
import editstudentComponent from '../views/editstudent.vue'
import kurslistComponent from '../views/kurslist.vue'
import kursdetailsComponent from '../views/kursdetails.vue'

Vue.use(VueRouter)

export default new VueRouter({
    routes: [
        {
            path: '/',
            redirect: {
                name: "login"
            }
        },
        {
            path: "/login",
            name: "login",
            component: LoginComponent
        },
        {
            path: "/secure",
            name: "secure",
            component: SecureComponent
        },
        {
            path: '/student/details/:id',
            name: 'studentdetails',
            component: studentdetailsComponent
        },
        {
            path: '/student/edit/:id',
            name: 'editstudent',
            component: editstudentComponent
        },
        {
            path:'/kurs',
            name: 'kurs',
            component: kurslistComponent
        },
        {
            path: '/kurs/details/:id',
            name: 'kursdetails',
            component: kursdetailsComponent
        }
    ]
})