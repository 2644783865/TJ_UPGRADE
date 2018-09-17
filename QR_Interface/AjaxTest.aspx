<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="AjaxTest.aspx.cs" Inherits="ZCZJ_DPF.QR_Interface.AjaxTest" Title="无标题页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

<script type="text/javascript" language="javascript">
      var formdata={
					"DzCPQROutData":"2;热工;15798;17165BJ1-4;4565788;山东金鱼;辊压机;R2493;钢铁;TCB-2-F-14-2;3;台;12356.00;12.0000;账内;否;3;暂无匹配项!;统计;空;;",
					"DzCPQROutNum":"3",
					"TaskCode_Out":"372647846",
					"RealAddrs_Out":"csjhcbahsca",
					"DfReason_Out":"uecjwkcbhbhewbckq",
					"Note_Out":"tr7r7",
					"DzCPQROutPerson":"常颖"								
				}
      function ruku(){
           $.ajax({
                            type: "POST",
                            url: 'Daizhi_Finished_QROutAjaxHandler.aspx',
							data:formdata,
							dataType:'text',//服务器返回text格式数据
							type:'post',//HTTP请求类型            
							success:function(data){
									alert("成功");
							}
					});
      }
</script>


<div>
     <input type="button" value="入库" onclick="ruku()" />
</div>
</asp:Content>
