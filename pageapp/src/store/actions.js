export default{
    storeToken:({commit}, token) => {
        commit('addToken', token)
    },
    storeStudents:({commit}, students) => {
        commit('addStudents', students)
    }
}