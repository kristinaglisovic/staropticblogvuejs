<template>
  <v-app id="front">
    <NavBar/>
    <v-main class="p-0">

      <router-view/>

    </v-main>
         
         <!-- <Dataget></Dataget> -->
    <Footer></Footer>
  </v-app>
</template>




<script scoped>
import NavBar from '../../components/Home/NavBar.vue';
import Footer from '../../components/Home/Footer.vue';



export default {
  name: 'Front',
  components:{
    NavBar,
    Footer,
    },
  

  
};
</script>

<style scoped>
#front{
  background-image: url(../../../public/img/bg.jpg);
  background-size:cover;
}
</style>