<template>
    <div class="comment">
       
        <div class="avatar">
              <v-icon @click='deleteComment(comment.id)' size="15px" v-if="authenticated && comment.user==user.username" class="p-2 mr-3 shadow rounded-circle bg-danger ">fa-trash-can
              </v-icon>
            <template v-if="comment.image">
            <img v-bind:src="'http://localhost:5000/Images/'+comment.image">
             </template>
        
             <template v-else>
                 <v-icon size="40">mdi-account-circle</v-icon>
                 </template>
             </div>

        <div class="text">
            <span class="username">@{{ comment.user }}</span> <span class="komentar">{{ comment.comment }}</span>
           
        </div>
 
    </div>
</template>

<script>
  import axios from "axios"
  import { mapGetters } from 'vuex';
    export default {
        name: 'singleComment',
        props: ['comment'],
         computed:{
      ...mapGetters({
            authenticated:'auth/authenticated',
            user:'auth/user'
          })
         },
        methods: {
             deleteComment(id){
             //console.log(id)
                        this.$emit('delete-comment', id);
             } 
            
        },
        
         
    }
    
</script>

<style scoped>
/* Single-comment component */
.comment {
    display: flex;
    padding: 10px;
    margin-bottom: 10px;
    align-items: center;
    color: #333;
    background-color: #F2F2F2;
    border-radius: 30px;

    box-shadow: 1px 1px 3px rgba(0, 0, 0, 0.2);
}



 
@media only screen and (max-width: 487px) {
    .comment .text {
        max-width: 200px;
    }
 
}

@media only screen and (max-width: 363px) {
    .comment .text {
        max-width: 100px;
    }
 
}
  
.comment .avatar {
    align-self: flex-start;
}

.komentar{
      word-wrap: break-word !important;
}

.comment .avatar > img {
    width: 40px;
    height: 40px;
    border-radius: 100%;
    align-self: start;
}

.comment .text {
    text-align: left;
    margin-left: 5px;
 
}

.comment .text span {
    margin-left: 5px;
}

.comment .text .username {
    font-weight: bold;
    color: #333;
}

</style>