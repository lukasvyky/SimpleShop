var app = new Vue({
    el: '#app',
    data: {
        price: 0,
        showPrice: true
    },
    computed: {
        formatPrice: function () {
            return "CZK" + this.price;
        }  
    },
    methods: {
        togglePrice: function () {
            this.showPrice = !this.showPrice;
        },
        alert: function (m) {
            alert(m);
        }
    }
});