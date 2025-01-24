<template>
  <q-page class="row items-center justify-evenly" style="background-color: #f5f5f5;">
    <div>
      <div class="column items-center no-select" style="height: 150px;">
        <div class="flex text-center">
          <div class="login-header-title-label no-warp red-hat-normal-font">RERIS</div>
        </div>
        <div class="flex text-center">
          <div class="login-header-subtitle-label red-hat-normal-font">Robotics Interface System</div>
        </div>
      </div>

      <br><br>
      <div class="column" style="height: 70px;">
        <div class="col red-hat-normal-font">
          <div class="red-hat-normal-font">Security Code</div>
          <div class="q-gutter-md" style="width: 350px;">
            <q-input rounded outlined bg-color="white" />
          </div>
        </div>
      </div>

      <br>
      <div class="column items-center" style="height: 150px;">
        <div class="login-header-forget-label red-hat-normal-font">
          <div @click="goToSignIn">If you already have an account, click here.</div>
          <br>
          <div>Forgot password?</div>
        </div>
        <br>
        <div class="flex text-center">
          <q-btn class="login-header-button-sign-in red-hat-normal-font" no-caps push color="" label="Sign up"
            style="width: 100px; background-color: #4BCC53;" />
        </div>
      </div>
    </div>
  </q-page>
</template>

<script setup lang="ts">
// import { ref } from 'vue';
import { useRouter } from 'vue-router';
import '../css/login.css';

// Reactive options array for q-select
// const options = ref(['Member']);
const router = useRouter();

const goToSignIn = async () => {
  try {
    await router.push('/login'); // Replace '/sign-up' with your actual sign-up route
  } catch (error) {
    console.error('Navigation error:', error);
  }
};

</script>
