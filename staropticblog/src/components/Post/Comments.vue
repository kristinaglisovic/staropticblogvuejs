<template>
    <div class="comments sivaPozadina">
     
        <div :class="comments_wrapper_classes">
           <template v-if="comments.length>0">
            <single-comment 
                v-for="comment in comments"
                :comment="comment"
                :key="comment.id"
                @delete-comment="deleteComment"
            ></single-comment>
                </template>

                <template v-else>
              <p class="text-center">Trenutno ne postoji nijedan komentar na ovoj objavi.</p>
              </template>
        </div>

        <hr>
        <template v-if="authenticated">
        <v-form ref="form" v-model='isFormValid' @submit.prevent="submitComment">
        <div class="reply">
            <div class="avatar">
                  <template v-if="user.image!='No image'">
                <img v-bind:src="'http://localhost:5000/Images/'+user.image" alt="">
                </template>
                  <template v-else>
                             <v-icon size="40">mdi-account-circle</v-icon>
                </template>
                
            </div>
            
                       <v-text-field
                          class="reply--text" 
                          color="gray"
                          counter="true"
                          label="Ostavite komentar...."
                          v-model.trim="reply"
                          :rules="komentarRule"
            ></v-text-field>
     
            
        </div>
        <div class="text-center pt-5">

            <v-btn class="reply--button" type="submit" :disabled="!isFormValid"><i class="fa fa-paper-plane"></i> Postavi</v-btn>
        </div>

        </v-form>
        </template>
        <template v-else>
             <div class="reply justify-content-center">
                <p class="pt-3 fw-bold">Morate biti prijavljeni kako biste ostavili komentar!</p>
             </div>
        </template>
    </div>
</template>

<script>
import singleComment from './SingleComment'

  import { mapGetters} from 'vuex';
    export default {
        name: 'comments',
        components: {
            singleComment
        },
        data: () => ({
            isFormValid:true,
            komentarRule: [
             t => !!t || 'Komentar ne sme biti prazan',
             l=> !l || l.length<=40 || 'Komentar može imati najviše 40 karaktera.'
           ],
            reply: ''
        }),
        methods: {
            submitComment() {
                if(this.reply != '') {
                    this.$emit('submit-comment', this.reply);
                    this.$refs.form.reset()
                }
            },
             deleteComment(id){
             //console.log(id)
                        this.$emit('delete-comment', id);
             } 
        },
        props: ['comments', 'current_user', 'comments_wrapper_classes'],
        computed:{
              ...mapGetters({
               authenticated:'auth/authenticated',
               user:'auth/user'
          })
          }
    }
</script>

<style scoped>
.comments {
    padding: 20px;
    padding-top: 20px;
}

.comments-wrapper {
    max-height: 250px;
    overflow-y: auto;
    padding-right: 10px;
}

.custom-scrollbar::-webkit-scrollbar-track
{
    -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.3);
    -moz-box-shadow: inset 0 0 6px rgba(0,0,0,0.3);
    box-shadow: inset 0 0 6px rgba(0,0,0,0.3);
    border-radius: 10px;
    background-color: #fff;
}

.custom-scrollbar::-webkit-scrollbar
{
    width: 8px;
    background-color: #fff;
}

.custom-scrollbar::-webkit-scrollbar-thumb
{
    border-radius: 10px;
    -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,.3);
    -moz-box-shadow: inset 0 0 6px rgba(0,0,0,.3);
    box-shadow: inset 0 0 6px rgba(0,0,0,.3);
    background-color: #555;
}

/* Reply component */
.reply {
    display: flex;
    position: relative;
    align-items: center;
    background-color: #EBEBEB;
    border-radius: 30px;
    padding: 5px 10px;
    overflow: hidden;
}
.sivaPozadina{
     background-color: #C8C8C8 !important; 
}

.reply .avatar {
    position: absolute;
}

.reply .avatar > img {
    width: 40px;
    height: 40px;
    border-radius: 100%;
}

.reply--text {
    min-height: 40px;
    padding: 10px 10px 10px 55px;
    margin-right: 10px;
    border: 0;
    color: #333;
    width: 100%;
    outline: 0;
    background-color: transparent;
    box-shadow: none;
}


.reply--button{
     background-color: rgb(196, 196, 196);
}

.reply--button:hover {
    color: #fff;
    background-color: #2a629c;
}

.reply--button:focus,
.reply--button:active {
    box-shadow: 0 0 0 0.2rem rgba(0, 123, 255, 0.25);
}

hr {
    margin-top: 10px;
    margin-bottom: 10px;
}
</style>