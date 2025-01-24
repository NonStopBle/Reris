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

      <div class="column" style="height: 150px;">
        <div class="col red-hat-normal-font">
          <div class="login-username-label red-hat-normal-font">Username or Email</div>
          <div class="q-gutter-md" style="width: 350px;">
            <q-input rounded outlined bg-color="white" />
          </div>
        </div>
        <br><br>
        <div class="col red-hat-normal-font">
          <div class="red-hat-normal-font">Password</div>
          <div class="q-gutter-md" style="width: 350px;">
            <q-input rounded outlined bg-color="white" />
          </div>
        </div>
      </div>

      <br><br>
      <div class="column" style="height: 50px;">
        <div class="col red-hat-normal-font">
          <div class="login-username-label red-hat-normal-font">Confirm Password</div>
          <div class="q-gutter-md" style="width: 350px;">
            <q-input rounded outlined bg-color="white" />
          </div>
        </div>
        <br><br>

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
          <div>We need your security code to verify your identication !</div>
        </div>
        <br>
        <div class="flex text-center">
          <q-btn class="login-header-button-sign-in red-hat-normal-font" no-caps push color="" label="Reset Password"
            style="width: 140px; background-color: #4BCC53;" />
        </div>
      </div>
    </div>
  </q-page>
</template>

<script setup lang="ts">
// import { ref } from 'vue';
import '../css/login.css';

// Reactive options array for q-select
// const options = ref(['Member']);
</script>
