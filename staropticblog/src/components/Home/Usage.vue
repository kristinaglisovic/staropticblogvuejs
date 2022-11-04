<template>
<div class="container-fluid py-5 siva">
    
    <h4 class="fw-bold text-center bela py-5" v-scrollanimation>JER JE RAZLIKA OČIGLEDNA!</h4>
    <p class="text-center bela fw-bold" v-scrollanimation>Svakog dana činimo da pogled na svet bude lepši i bolji.</p>
    <p class="text-center bela fw-bold" v-scrollanimation>Kako?</p>
    <p class="text-center"><v-btn class='fw-bold bg-warning' elevation="2" small v-scrollanimation to="/blog">Pogledajte sami i uverite se</v-btn></p>
</div>
</template>
<style scoped>

h4{
    font-size: 60px;
   
    
}

.bela{
 color:#FFEFEF;
 text-shadow: 1px 2px 5px #666;
}

@media only screen and (max-width: 956px) {
    h4{
        font-size: 45px;
    }
}
@media only screen and (max-width: 515px) {
    h4{
        font-size: 30px;
    }
}


.siva{
background: linear-gradient(0deg, rgba(124,153,172,1) 0%, rgba(146,169,189,1) 79%);
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