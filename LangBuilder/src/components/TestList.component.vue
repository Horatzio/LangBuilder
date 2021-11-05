<template>
    <draggable :list="testArray">
        <li v-for="item in testArray" :key="item.id">
            {{item.text}}
        </li>
    </draggable>
</template>


<script>

import Draggable from 'vuedraggable';

export default {
    components: {
        Draggable
    },
    data: function() {
        return {
            testArray: []
        }
    },
    mounted: function() {
        for(let i = 0 ; i < 10; i++){
            this.testArray.push({
                id: i,
                text: "test-" + i
            });
        }
    }
}
</script>


<style scoped>

</style>