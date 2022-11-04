<template>
  <div class="pt-5">
    <v-container v-if="mounted">
     
      <div class="row d-flex flex-wrap text-wrap">
        <div class="col-md-12 col-sm-12 col-xs-12 flex-wrap text-wrap">
          <div class="d-flex justify-content-around align-items-center flex-wrap pb-5">
            <div class="p-4">
              

                <h1 class="fw-bold text-uppercase	text-center pa-4 text-wrap">{{post.title}}</h1>
                <p class="overline text-center"><i class="fa-solid fa-user text-wrap"></i> <b>{{post.author}}</b> | <i class="fa-solid fa-clock"></i> {{post.createdAt | formatDate}}</p>
                   <p class="pt-5 text-wrap text-center fw-bold">{{post.description}}</p>
             
             
               <template v-if="authenticated">
                    <v-divider></v-divider>
                    <!-- <span v-if="like=='liked'"><i class="fa-solid fa-heart"></i></span> -->
                    <div class="text-center">
                      <template  v-if="post.likesInfo.find(l => l.userId == user.id)">

                      <v-btn @click="likePost" class="bg-danger"><i class="fa-solid fa-heart-crack"></i></v-btn>
                      </template>
                      <template v-else>
                        <v-btn @click="likePost" class="bg-warning"><i class='fa-solid fa-heart'></i></v-btn>
                      </template>
                    </div>

               </template>
            
            </div>
            
            <v-img
              class="white--text align-end slika shadow"
              v-bind:src="'http://localhost:5000/Images/'+post.images[0]"
              max-width="900"
             ></v-img>


             
          </div>
          
          <v-divider></v-divider>
          <div class="py-5">
            <h5 class="text-center">Tagovi</h5>
          <div class="d-flex align-items-center justify-content-center flex-wrap">   
            
            
            <span v-for='t in post.tags' class="badge rounded-pill bg-dark m-1"><i class='fa-solid fa-hashtag'></i> {{t}}</span>
          
          </div>
          </div>
          
          <v-divider></v-divider>

          <div class="pb-5">
            <h5 class="text-center">Kategorije</h5>
          <div class="d-flex align-items-center justify-content-center flex-wrap">   
            
            
            <span v-for='c in post.categories' class="badge rounded-pill bg-dark m-1"><i class='fa-solid fa-c'></i> {{c}}</span>
          
          </div>
          
          <v-divider></v-divider>


            <h2 class="mb-0 pt-5 text-center text-uppercase" v-if='post.heading1' >{{post.heading1}}</h2>
         
            <h3 v-if='post.text1'  class="text-center py-5 fw-light">{{post.text1}}
  
             
            </h3>

          <v-divider  v-if='post.text1'></v-divider>


            <template v-if="post.images.length>1">
              <h2 class="text-center pb-2">Galerija</h2>
              <h3 class="font-weight-light text-center pb-2"><i class="fa-solid fa-camera"></i></h3>
          <div class="container shadow-sm galerija w-50 mb-5">
            <carousel-3d 
                    :controls-visible="true"
                    :clickable="false"
                    :key="post.images.length"
                    :height="220"
                    >
                    <Slide :index="i" :key="i" v-for="(img,i) in post.images" class="shadow-sm">
                        <figure>
                            <img :src="'http://localhost:5000/Images/'+img"/>
                        </figure>
                    </Slide>
                    </carousel-3d>
          </div>

                    </template>


                    <v-divider  v-if='post.heading2'></v-divider>
          <h2 v-if='post.heading2' class="mb-0 pt-5 text-center text-uppercase">{{post.heading2}}</h2>
            <h3 v-if='post.text2'  class="text-center py-5 fw-light">{{post.text2}}
  
             
              </h3>
            <v-divider  v-if='post.text2'></v-divider>

            <h2 class="text-center py-2">Komentari</h2>
              <h3 class="font-weight-light text-center pb-2"><i class="fa-solid fa-comments"></i></h3>
              <div class='mb-5'>
            <div class="comments-outside">
                  <div class="comments-header">
                    <div class="comments-stats">
                        <span><i class="fa fa-thumbs-up"></i> {{ post.likes }}</span>
                        <span><i class="fa fa-comment"></i> {{ post.commentsCount }}</span>
                      </div>
                  
                      <div class="post-owner">
                        <div class="avatar">
                          <template v-if="authenticated && user.image!='No image'">
                          <img v-bind:src="'http://localhost:5000/Images/'+user.image">
                          </template>
                          <template v-else>
                             <v-icon size="30">mdi-account-circle</v-icon>
                          </template>
                        </div>
                        <template v-if="authenticated">
                        <div class="username">
                          <span>@{{ user.username }}</span>
                        </div>
                        </template>
                      </div>
                   
                    </div>

                    

                    <comments 
                      :comments_wrapper_classes="['custom-scrollbar', 'comments-wrapper']"
                      :comments="post.comments"
                      :current_user="user"
                      @submit-comment="submitComment"
                      @delete-comment="deleteComment"
                    >
                   
                    </comments>
                      <template v-if="feedback!=''">
                            
                               <p class="text-danger text-center fidb fw-bold py-3">{{feedback}}</p>
                            
                      </template>
                     


               </div>
              </div>
        </div>
      </div>
      </div>
    </v-container>
  </div>
</template>
<script>
    import axios from "axios"
    import { mapGetters} from 'vuex';
    import {Carousel3d,Slide} from "../../../node_modules/vue-carousel-3d"
import Comments from "../../components/Post/Comments.vue"
    export default {
        
        data: () => ({

          
            post:null,
            mounted:false,
            text:'',
            feedback:'',
            UserId:''
            
        }),
         mounted: function() {

            this.loadPost()
          },
           methods: {
                loadPost(){
                       axios.get(`posts/${this.$route.params.id}`)
                     .then(response => {
                        console.log(response.data);
                        this.post=response.data;
                        this.mounted=true;
                    })
                     .catch(error => {
                        if (error.response) {
                          //response status is an error code
                      //console.log(error.response.status);
                      if(error.response.status==404){
                        this.$router.push('/blog')
                      }
  
                       }
                     else if (error.request) {
                      //response not received though the request was sent
                          console.log(error.request);
                      }
                       else {
                              //an error occurred when setting up the request
                      console.log(error.message);
                  }
               })
                
                  },


                  likePost(){
             //console.log(id)

               axios.patch('posts/like',{
                       UserId:this.user.id,
                       PostId:this.$route.params.id,
                      }
                    )
                    .then(()=>{
                        this.loadPost()
                       
                    })
                    .catch(err=>{
                      console.log(err)
                    // console.log(this.feedback)
                    })
 
               },


               submitComment: function(reply) {
                 axios.post('comments',{
                       UserId:this.user.id,
                       PostId:this.$route.params.id,
                       Text:reply
                      }
                    )
                    .then(()=>{
                        this.loadPost()
                        this.feedback=''
                    })
                    .catch(err=>{
                    //  console.log(err)
                      this.feedback=err.response.data.message;
                    // console.log(this.feedback)
                    })
               
              },

               deleteComment:function(id){
                  //console.log(id)

               axios.delete('comments/'+id)
                    .then(()=>{
                     
                       this.loadPost()
                    })
                    .catch(err=>{
                      console.log(err)
                    })

             }   

                    
                  

                  
              },
                      
         
          components:{
    Carousel3d,
    Slide,
    Comments
},
            computed:{
              ...mapGetters({
               authenticated:'auth/authenticated',
               user:'auth/user'
          })
    },
 
         

    }
</script>
<style scoped>
.carousel-3d-container figure{
    margin: 0;
}

@media only screen and (max-width: 1264px) {
    .galerija{
      width: 100% !important;
    }
 
}

@media only screen and (max-width: 768px) {
    .slika{
      max-width: 100% !important;
    }
 
}
@media only screen and (max-width: 959px) {

    .forma{
      width: 100% !important;
    }
 
}
.v-list{
  max-height: 200px;
  overflow-y: auto;

}


hr {
  display: block;
  height: 1px;
  border: 0;
  border-top: 1px solid #ececec;
  margin: 1em 0;
  padding: 0;
}
.comments-outside {
  box-shadow: 2px 2px 8px rgba(0, 0, 0, 0.3);
  margin: 0 auto;
  max-width: 1000px;
}
.comments-header {
  background-color: #C8C8C8;
  padding: 10px;
  align-items: center;
  display: flex;
  justify-content: space-between;
  color: #333;
  min-height: 80px;
  font-size: 20px;
}
.comments-header .comments-stats span {
  margin-left: 10px;
}
.post-owner {
  display: flex;
  align-items: center;
}
.post-owner .avatar > img {
  width: 30px;
  height: 30px;
  border-radius: 100%;
}
.post-owner .username {
  margin-left: 5px;
}
.post-owner .username > a {
  color: #333;
}

.fidb{
  background-color: #C8C8C8;
}


.container{
  word-break: normal !important;
}

</style>
