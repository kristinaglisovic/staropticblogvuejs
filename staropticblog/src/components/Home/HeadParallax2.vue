<template>
  <v-parallax
    height="700"
    dark
    src="../../../public/img/banner.png"
  >
    <v-row
      align="center"
      justify="center"
    >
      <v-col   
        class="text-center"     
        cols="12"
      >
        <h1  v-scrollanimation class="font-weight-bold mb-3 siva">„OČI SU OGLEDALO DUŠE“
        </h1>
        <div v-for="t in eyes">
        <p  class='fw-bold siva' v-scrollanimation>{{t}}</p>
        </div>
      </v-col>
    </v-row>
  </v-parallax>
</template>

<script>
export default {
  data: () => ({
      eyes: [
        'Kada pitate nekoga šta prvo primeti na nekoj osobi, često se čuje odgovor: OČI.',
        'Obično se misli na boju očiju, oblik oka ili slično.',
        'U pogledu možemo prepoznati sve „nijanse“ raspoloženja.',
        'GLEDATI I VIDETI NIJE ISTO.',
       
      ],
    }),
  
}
</script>


<style scoped>
  h1{
    font-size:60px;
  }

  .siva{
    color:#7C99AC;
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