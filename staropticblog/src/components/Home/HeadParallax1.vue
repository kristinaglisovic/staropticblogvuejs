<template>
  <v-parallax 
    height="700"
    dark
    src="../../../public/img/hero.png"
  >
    <v-row
      align="center"
      justify="center"
    >
      <v-col   
        class="text-left"     
        cols="11"
      >
        <h1 class="font-weight-bold mb-4" v-scrollanimation>Star Optik Blog
        </h1>
        <h2 v-scrollanimation>
         Sve što bi trebalo da znate o naočarima i vidu
        </h2>
        <h3 v-scrollanimation>(a nikada Vam ne bi palo na pamet) <i class="fa-solid text-warning fa-face-smile-wink"></i></h3>
      </v-col>
    </v-row>
  </v-parallax>
</template>

<script>
export default {
  
}
</script>


<style scoped>
  h1{
    font-size:60px;
  }


  .before-enter{
    opacity: 0;
    transform: translateY(100px);
    transition: 2s ease-in-out;
  }

  .enter{
   opacity: 1;
    transform: translateY(0px);
  }
</style>