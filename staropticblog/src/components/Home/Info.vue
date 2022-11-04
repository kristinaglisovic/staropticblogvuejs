<template>
  <v-carousel :show-arrows="false" cycle interval="3500" v-model="model" height="600">
    
    <v-carousel-item
        
        v-for="(n,i) in infoImages"
           :key="n.src"
          :cols="n.src"
          :src="require('../../../public/img/' + n.src + '.jpg')" 
          
    >
        <v-overlay class="position-absolute over"
          :opacity="0.7"
        > </v-overlay>
        <v-row
          class="fill-height"
          align="center"
          justify="center"
        >
          <div class="text-center textt text-uppercase p-3">
            <h3 v-scrollanimation class="text-white h1 fw-bold senka mb-4">{{n.title}}</h3>
            <p v-scrollanimation class="text-white  fw-bold senka">{{n.text}}</p>
            <p v-scrollanimation class="text-white fw-bold senka">{{n.text2}}</p>
          </div>
        </v-row>
    </v-carousel-item>
     
  </v-carousel>

</template>

<style scoped>
.textt{
  z-index:2 !important;
}
.over{
  z-index:1 !important;
}
.senka{
  text-shadow: 2px 3px 5px rgb(61, 61, 61);
  letter-spacing: 1.4px;
}

@keyframes fade-in {
  100% {
    opacity: 1;
    filter: blur(0);
  }
}


  .before-enter{
   
     filter: blur(4px);
  }
  .enter{

    animation: fade-in 0.25s 0.25s forwards cubic-bezier(0.11, 0, 0.5, 0)
  }
  
</style>


<script>
  export default {
    data: () => ({
      model: 0,
      infoImages: [
        {src:'featured-pic-1',title:'SUNČANE NAOČARE',text:"Da li su sunčane naočare samo modni detalj?",text2:"Da li samo pratimo modne trendove, ili znamo zašto su baš u ovom trenutku aktuelni određeni oblici i boje?"},
        {src:'featured-pic-2',title:'DIOPTRIJSKE NAOČARE',text:"Naočare sa najviše benefita",text2:"Ako imamo problem sa vidom, bez njih ne možemo zamisliti dan."},
        {src:'featured-pic-3',title:'ZAŠTITA',text:"Veliki broj različitih zaštitinih slojeva",text2:"Naše oči su izložene nepovoljnim uticajima: kako iz prirode, tako i od tehnologije, koja je svuda oko nas."},
      ],
    }),
  }
</script>