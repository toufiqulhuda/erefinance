function generatePDF(divIdName, reportName) {
    var element = document.getElementById(divIdName);
    //alert(element);
    var opt = {
        margin: [30, 10, 5, 10], // top, left, buttom, right
        filename: reportName + '.pdf',
        //image: { type: 'jpeg', quality: 0.98 },
        //html2canvas: { scale: 2 },
        pagebreak: { aviod: 'tr' },
        jsPDF: { orientation: 'portrait', unit: 'pt', format: 'A4', compressPDF: true }
    };
    html2pdf().from(element).set(opt).save();
    //html2pdf().from(element).set({
    //    margin: [30, 10, 5, 10], // top, left, buttom, right
    //    pagebreak: { aviod: 'tr' },

    //    jsPDF: { orientation: 'portrait', unit: 'pt', format: 'A4', compressPDF: true }
    //}).save();
}