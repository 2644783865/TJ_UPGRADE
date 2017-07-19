<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true"
    CodeBehind="QC_InspecTaskAssign.aspx.cs" Inherits="ZCZJ_DPF.QC_Data.QC_InspecTaskAssign"
    Title="无标题页" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link href="../FM_Data/StyleFile/model.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" charset="GB2312">

    //**********弹出技术部人员子窗口***********************
    function SelTechPersons()
    {
       var i=window.showModalDialog('QC_Task_Persons.aspx','',"dialogHeight:400px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no"); 
      if(i!=null)
      {
       document.getElementById('<%=txtqcclerknmid.ClientID%>').value=i[0];
       document.getElementById('<%=txtqcclerknm.ClientID%>').value=i[1];
      }
    }
      function okay() 
    {
        window.parent.document.getElementById('ButtonViewDone').click();
    }
    function cancel() 
    {
        window.parent.document.getElementById('ButtonViewCancel').click();
    }
    
    
    </script>

    <div class="popup_Container">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <div class="TitlebarRight" onclick="cancel();">
                    </div>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <div class="popup_Body">
                    <div style="width: 80%">
                        <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1"
                            width="100%">
                            <tr>
                                <td width="30%" align="right">
                                    质检员:
                                </td>
                                <td width="70%">
                                    <input id="txtqcclerknm" type="text" value="" readonly="readonly" runat="server" />
                                    <input id="txtqcclerknmid" runat="server" readonly="readonly" type="text" value=""
                                        style="display: none" />
                                    <asp:HyperLink ID="hySel" runat="server" CssClass="hand" onClick="SelTechPersons()">
                                        <asp:Image ID="imgSel" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle"
                                            runat="server" />
                                        选择
                                    </asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnConfirm" runat="server" Text="确  定" OnClick="btnConfirm_Click" />
                                 
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
