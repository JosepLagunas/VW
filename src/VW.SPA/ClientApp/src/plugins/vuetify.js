import Vue from 'vue'
import {
    Vuetify,
    VApp,
    VNavigationDrawer,
    VFooter,
    VList,
    VBtn,
    VIcon,
    VGrid,
    VToolbar,
    VDataTable,
    VProgressLinear,
    transitions
} from 'vuetify'
import 'vuetify/src/stylus/app.styl'
import VTextarea from "vuetify/lib/components/VTextarea/VTextarea";
import VTextField from "vuetify/lib/components/VTextField/VTextField";
import VBtnToggle from "vuetify/lib/components/VBtnToggle/VBtnToggle";
import VCard from "vuetify/lib/components/VCard/VCard";
import VChip from "vuetify/lib/components/VChip/VChip";
import VDialog from "vuetify/lib/components/VDialog/VDialog";
import {VCardActions, VCardText} from "vuetify/lib/components/VCard";
import VCardTitle from "vuetify/lib/components/VCard/VCardTitle";
import VDivider from "vuetify/lib/components/VDivider/VDivider";
import VSwitch from "vuetify/lib/components/VSwitch/VSwitch";
import VTooltip from "vuetify/lib/components/VTooltip/VTooltip";


Vue.use(Vuetify,
    {
        components: {
            VApp,
            VNavigationDrawer,
            VFooter,
            VList,
            VBtn,
            VIcon,
            VGrid,
            VToolbar,
            VDataTable,
            VProgressLinear,
            transitions,
            VTextarea,
            VTextField,
            VBtnToggle,
            VCard,
            VCardActions,
            VCardText,
            VCardTitle,
            VChip,
            VDialog,
            VDivider,
            VSwitch,
            VTooltip            
        },
        theme: {
            primary: '#1976D2',
            secondary: '#424242',
            accent: '#82B1FF',
            error: '#FF5252',
            info: '#2196F3',
            success: '#4CAF50',
            warning: '#FFC107'
        }
    });