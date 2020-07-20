export default{
    getStudentById: (state) => (id) => {
        return state.allStudents.find(student => student.StudentId === id)
      }
}