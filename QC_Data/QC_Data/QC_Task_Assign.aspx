   
<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="QC_Task_Assign.aspx.cs" Inherits="ZCZJ_DPF.QC_Data.QC_Task_Assign" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
    
    <asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    质量分工
</asp:Content>
    
    

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <link href="../FM_Data/StyleFile/model.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

    function SelTechPersons3()
    {
      
       var i=window.showModalDialog('QC_Task_Persons.aspx?id=0','',"dialogHeight:400px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no"); 
       if(i!=null)
      {
        document.getElementById('<%=txtqcclerkid.ClientID%>').value=i[0];
        document.getElementById('<%=txtqcclerk.ClientID%>').value=i[1];
      }
    }
    </script>

    <div class="popup_Container">
    <%--黄色--%>
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <div class="TitlebarRight" onclick="cancel();">
                    </div>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
         <%--白色100%--%>
            <div class="box-outer">
                <div class="popup_Body">
                    <asp:Panel Width="80%" runat="server">
                        <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1"
                            width="100%">
                           
                            <tr>
                                <td class="tdleft">
                                    质检员:
                                </td>
                                <td class="tdright">
                                    <input id="txtqcclerk" runat="server"  type="text" value="" />
                                    <input id="txtqcclerkid" runat="server" type="text" value=""
                                        style="display: none" />
                                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="hand" onClick="SelTechPersons3()">
                                        <asp:Image ID="Image1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle"
                                            runat="server" />
                                        选择
                                    </asp:HyperLink>
                                </td>
                            </tr>
                          
                            <tr>
                            <td class="tdleft">
                                备注：
                            </td>
                            <td class="tdright">
                            <asp:TextBox ID="bz" runat="server" TextMode="MultiLine" Width="400px" Height="100px"></asp:TextBox>
                            </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                  
                                    <asp:Button ID="btnConfirm" runat="server" OnClick="btnConfirm_Click" Text="确  定" />
                                    <asp:Button ID="btnCancel" runat="server" Text="作废停工" OnClick="btnCancel_Click" />
                                    <asp:Button ID="btnClose" runat="server" Text="关闭" onclick="btnClose_Click" />
                                </td> 
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
