var app = new Vue({
    el: '#app',
    data: {
        username: ""
    },
    computed: {
    },
    mounted() {
        //TODO: get all users
    },
    methods: {
        createUser() {
            axios.post("/users", { username: this.username })
                .then(res => {
                    console.log(res);
                })
                .catch(err => {
                    console.log(err);
                });
        }
    }
});