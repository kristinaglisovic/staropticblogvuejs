<template>
<div class="home">
  <HeadParallax1></HeadParallax1>
  <HeadParallax2></HeadParallax2>

  <Info></Info>
  <Usage></Usage>

</div>
</template>

<script>
import HeadParallax1 from '@/components/Home/HeadParallax1.vue';
import HeadParallax2 from '@/components/Home/HeadParallax2.vue';
import Info from '@/components/Home/Info.vue';
import Usage from '@/components/Home/Usage.vue';

export default {
    name: 'Home',
    components: {
      HeadParallax1,
      HeadParallax2,
      Info,
      Usage

    },
    // computed: {
    //   ...mapGetters(['user'])
    // }


    
  }
</script>
