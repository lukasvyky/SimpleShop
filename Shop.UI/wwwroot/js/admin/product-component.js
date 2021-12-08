Vue.component('product-manager', {
    template: `<div>
            <div v-if="!editing">
                <table class="table">
                    <tr>
                        <th>Id</th>
                        <th>Product</th>
                        <th>Value</th>
                        <th></th>
                        <th></th>
                    </tr>
                    <tr v-for="(product, index) in products">
                        <td>{{product.id}}</td>
                        <td>{{product.name}}</td>
                        <td>{{product.value}}</td>
                        <td> <a @click="editProduct(product.id, index)">Edit</a></td>
                        <td> <a @click="deleteProduct(product.id, index)">Remove</a></td>
                    </tr>
                </table>
                <button class="button" @click="newProduct">Add new product</button>
            </div>

            <div v-else>
                <div class="field">
                    <label class="label">Product name</label>
                    <div class="control">
                        <input class="input" v-model="productModel.name" />
                    </div>

                    <div class="field">
                        <label class="label">Product description</label>
                        <div class="control">
                            <input class="input" v-model="productModel.description" />
                        </div>
                    </div>

                    <div class="field">
                        <label class="label">Product value</label>
                        <div class="control">
                            <input class="input" v-model="productModel.value" />
                        </div>
                    </div>
                </div>

                <button class="button is-success" @click="createProduct" v-if="!productModel.id">Create Product</button>
                <button class="button is-warning" @click="updateProduct" v-else>Update Product</button>
                <button class="button" @click="cancel">Cancel</button>
            </div>
       </div>`,
    data() {
        return {
            editing: false,
            objectIndex: 0,
            loading: false,
            productModel: {
                id: 0,
                name: "Product Name",
                description: "Product Description",
                value: 1
            },
            products: []
        }
    },
    computed: {
    },
    mounted() {
        this.getProducts();
    },
    methods: {
        getProduct(id) {
            this.loading = true;
            axios.get("/admin/products/" + id)
                .then(res => {
                    console.log(res);
                    var product = res.data;
                    this.productModel = {
                        id: product.id,
                        name: product.name,
                        description: product.description,
                        value: product.value
                    };
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
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
            this.editing = false;
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
                });
        },
        editProduct(id, index) {
            this.objectIndex = index;
            this.getProduct(id);
            this.editing = true;
        },
        updateProduct() {
            this.loading = true;
            this.editing = false;
            axios.put("/admin/products", this.productModel)
                .then(res => {
                    console.log(res);
                    this.products.splice(this.objectIndex, 1, res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        deleteProduct(id, index) {
            this.loading = true;
            axios.delete("/admin/products/" + id)
                .then(res => {
                    console.log(res);
                    this.products.splice(index, 1);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        newProduct() {
            this.editing = true;
            this.productModel.id = 0;
        },
        cancel() {
            this.editing = false;
        }
    }
});