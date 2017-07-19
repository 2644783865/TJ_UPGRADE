<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OM_ContractEditDialog.aspx.cs"
    Inherits="ZCZJ_DPF.OM_Data.OM_ContractEditDialog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>合同签订记录修改</title>

    <script src="../JS/EasyUI/jquery.min.js" type="text/javascript"></script>

    <script src="../JS/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>

    <script src="../JS/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>

    <link href="../JS/EasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../JS/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Contract_Data/StyleFile/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/OM_menu.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

         function validdate(){
            for(var  i =1; i < 59; i++) {
             var inputi=document.getElementsByTagName("input")[3*i].value;
             var inputiA=document.getElementsByTagName("input")[3*(i+1)].value;
             if((inputi!=null&&inputi!="")||(inputiA!=null&&inputiA!="")){
              $('#<%=inputmax.ClientID %>').val(i);
               } 
             else{
                 break;
               }
             }
           }
         var displayBar=true;
         function switchBar(obj)
         {
	        if (displayBar){
	          if(tab2.style.display =="none")
		       tab2.style.display="block";
		      if(tab2.style.display =="block"&&tab3.style.display =="none")
		       tab3.style.display="block";
		       displayBar=false;
		       obj.src="../Assets/images/bar_up.gif";
	         }
	        else{
		       displayBar=true;
		       obj.src="../Assets/images/bar_down.gif";
	        }
         }
    </script>

</head>
<body>
    <form runat="server">
    <div align="center">
        <asp:Panel runat="server" ID="PanInfo">
            <div class="box-wrapper" style="width: 470px;">
                <div style="height: 25px;" class="box_top" align="center">
                    <div style="margin-top: 1px">
                        <asp:Label ID="lbtop" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <div class="box-outer" id="DivRank" runat="server">
                    <div id="a" class="easyui-accordion" style="width: 359px; height: 446px;" align="center">
                        <div title="1-10次合同签订信息" data-options="selected:true" style="overflow: auto;">
                            <table id="tab1">
                                <tr>
                                    <td class="r_bg">
                                        <strong>第一次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS1" runat="server" class="easyui-datebox" Width="120px"> </asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE1" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第二次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS2" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE2" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第三次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS3" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE3" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第四次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS4" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE4" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第五次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS5" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE5" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第六次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS6" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE6" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第七次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS7" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE7" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第八次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS8" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE8" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第九次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS9" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE9" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第十次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS10" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE10" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div title="11-20次合同签订信息" style="overflow: auto;">
                            <table id="tab2">
                                <tr>
                                    <td class="r_bg">
                                        <strong>第11次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS11" runat="server" class="easyui-datebox" Width="120px"> </asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE11" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第12次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS12" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE12" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第13次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS13" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE13" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第14次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS14" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE14" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第15次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS15" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE15" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第16次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS16" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE16" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第17次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS17" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE17" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第18次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS18" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE18" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第19次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS19" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE19" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第20次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS20" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE20" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div title="21-30次合同签订信息" style="overflow: auto;">
                            <table id="tab3">
                                <tr>
                                    <td class="r_bg">
                                        <strong>第21次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS21" runat="server" class="easyui-datebox" Width="120px"> </asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE21" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第22次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS22" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE22" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第23次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS23" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE23" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第24次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS24" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE24" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第25次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS25" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE25" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第26次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS26" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE26" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第27次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS27" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE27" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第28次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS28" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE28" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第29次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS29" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE29" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="r_bg">
                                        <strong>第30次签订：</strong>
                                    </td>
                                    <td class="right_bg">
                                        <asp:TextBox ID="C_ContractS30" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>&nbsp;-
                                        <asp:TextBox ID="C_ContractE30" runat="server" class="easyui-datebox" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div align="center" style="margin-top: 15px">
                备注：
                <asp:TextBox ID="C_EditNote" TextMode="MultiLine" runat="server" Width="360px" Height="50px"></asp:TextBox><input
                    id="inputmax" type="text" runat="server" style="display: none" />
            </div>
            <div align="center" style="margin-top: 15px">
                <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="日期格式例：2015-01-01;2015.01.01;2015/01/01"></asp:Label>&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSave" runat="server" OnClientClick="validdate()" OnClick="btnSave_OnClick"
                    Text="修改 " BackColor="#ccffff" Width="40px" Height="25px" Font-Size="16px"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_OnClick" Text="取消" BackColor="#ccffff"
                    Width="40px" Height="25px" Font-Size="16px"></asp:Button>
            </div>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
