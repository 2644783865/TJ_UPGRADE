<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../JS/jquery/jquery.js" type="text/javascript"></script>
    <script src="../JS/jquery/ajaxfileupload.js" type="text/javascript"></script>

    <script type="text/javascript">
       

        function ajaxFileUpload() {
            $("#loading").ajaxStart(function() {
                $(this).show();
           }).ajaxComplete(function() { $(this).hide(); });
           $.ajaxFileUpload({
               url: 'ProcessHandler.ashx',
               secureuri: false,
               fileElementId: 'fileToUpload',
               dataType: 'json',
               success: function(data, status) {
               alert(data.msg);
               alert(data.guanlianTime);
               alert(data.filename);
                  
               },
               error: function(data, status, e) {
                   alert(e);
               }
           })
            return false;
        }
    </script>

</head>
<body>
    <form id="frm" enctype="multipart/form-data" method="post">
    <%-- <input type="file" name="fileToUpload" id="fileToUpload" />
    <input type="button" id="btnShangChuan" value="上传附件" />--%>
    <input id="fileToUpload" type="file" size="45" name="fileToUpload">
    <input type="button" id="buttonUpload" onclick="return ajaxFileUpload();" value="上传">
    </form>
</body>
</html>
