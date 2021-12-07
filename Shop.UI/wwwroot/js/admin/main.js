var app = new Vue({
    el: '#app',
    data: {
        loading: false,
        productModel: {
            name: "Product Name",
            description: "Product Description",
            value: 1
        },
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
        },
        createProduct() {
            this.loading = true;
            axios.post("/admin/products", this.productModel)
                .then(res => {
                    console.log(res);
                    this.products.push(res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                })
        }
    }
});