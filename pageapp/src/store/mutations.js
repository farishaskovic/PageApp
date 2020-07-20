export default{
    addToken:(state, token) => {
        state['accessTokenState'] = token
    },
    addStudents:(state, students) => {
        state['allStudents'] = students
    }
}