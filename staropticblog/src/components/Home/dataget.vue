<template lang="">
    <div>
       <h1>Data list</h1> 
    </div>
</template>
<script>

import axios from "axios"

export default {
    name:'test',
    // data () {
    //      return {
    //          info: null
    //     }
    // },
    mounted: function(){
            axios.get("http://localhost:8081/api/posts")
            .then(response => {
                console.log(console.log(response));
            })
            .catch(error => {
                if (error.response) {
                    //response status is an error code
                    console.log(error.response.status);
                }
                else if (error.request) {
                    //response not received though the request was sent
                    console.log(error.request);
                }
                else {
                    //an error occurred when setting up the request
                    console.log(error.message);
                }
        });
    }
}
</script>
<style lang="">
    
</style>