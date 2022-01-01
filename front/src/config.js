export default {
  DEFAULT_TITLE: "Mentol Provision",
  baseUrl: /*"http://192.168.80.230:5000"*/ '',
  baseUrlFront: "http://192.168.80.230:5001",
  vuetify: {
    elevation: 0,
    cols: {
      labelCols: 4,
    },
  },
  styles: {
    cardTextOverflow: "height: 560px; overflow-y: scroll",
  },
  form: {
    rowClass: "pl-3 pr-1 position-relative space-between",
    rowClass2: "pl-3 pr-1 position-relative space-between margin-bottom-info",
    rowClass3: "pl-2 pr-1 position-relative space-between",
    labelClass: "pl-0 pb-1 pt-1",
    labelClass2: "pl-0 pb-1 pt-1 pr-0",
    labelClass3: "pl-0 pb-1 pt-1 pr-0 mt-3",
    labelColClass: "pl-0 pt-0 pb-0 pr-0",
    labelColClass2: "pl-0 pt-0 pb-0 pr-0 width-176",
    fieldColClass: "pr-0 pt-0 pb-0",
    fieldColClass2: "pr-0 pt-0 pb-0 pl-0",
    fieldColClassNopadding: "pr-0 pt-0 pb-0 pl-0 width-421",
    fieldColClassNopadding2: "pr-0 pt-0 pb-0 pl-0 width-421 mr-5"
  },
  table: {
    dense: true,
    footerProps: {
      "items-per-page-options": [25, 50, 100, 200, 500]
    },
    hideDefaultFooter: true,
    fixedHeader: true,
    pageStepInfo: {
      class: "pt-3 pl-2 pb-3",
    },
    headerProps: {},
    pagination: {
      totalVisible: 7,
    },
    class: "",
  },
  colors: {
    white: "#fff",
    black: "#000",
    dark: "rgba(0, 0, 0, 0.87)",
    primary: "#1976d3",
    secondary: "#d4d5d9",
    accent: "#8c9eff",
    error: "#e64040",
    green: "green",
    yellow: "#FBBC1C",
    red: "red",
    pink: "pink",
    grey: "rgba(0, 0, 0, 0.25)"
  },
};
