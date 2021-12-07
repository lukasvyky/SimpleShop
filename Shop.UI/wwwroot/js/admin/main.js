var app = new Vue({
    el: '#app',
    data: {
        loading: false,
        products: []
    },
    computed: {
    },
    methods: {
        getProducts() {
            this.loading = true;
            axios.get("/admin/products")
                .then(res => {
                    console.log(res);
                    this.products = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        }
    }
});