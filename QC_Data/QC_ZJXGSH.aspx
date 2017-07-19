<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="QC_ZJXGSH.aspx.cs" Inherits="ZCZJ_DPF.QC_Data.QC_ZJXGSH" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<link href="../Contract_Data/StyleFile/Style.css" rel="stylesheet" type="text/css" />
<script language="javascript"  type="text/javascript" >
 function CheckBoxList_Click(sender) 
    {
        var container = sender.parentNode;        
        if(container.tagName.toUpperCase() == "TD") 
        // 服务器控件设置呈现为 table 布局（默认设置），否则使用流布局即为td
        { 
            container = container.parentNode.parentNode; // 层次： <table><tr><td><input />
        }        
        var chkList = container.getElementsByTagName("input");
        var senderState = sender.checked;
        for(var i=0; i<chkList.length;i++) {
            chkList[i].checked = false;
        }     
        sender.checked = senderState;          
    }
   
   function ShowViewModal(ID) {
     var date=new Date();
        var time=date.getTime();
        var retVal = window.open("QC_Inspection_Add.aspx?ACTION=VIEW&&back=1&&id="+ID);
    }
    
</script>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
            <table width="100%">
                    <tr>
                        
                        <td align="left">
                        <asp:Button ID="submit" runat="server"  OnClick="submit_click" />&nbsp
                        <asp:Button ID="Back" runat="server" Text="返回" OnClick="Back_click" />
                        <asp:Label ID="Notes" runat="server" Text="提交审核时请填写具体的质检修改内容" Visible="false"></asp:Label>
                        </td>
                        <td align="right">
                        
                        <asp:HyperLink ID="HyperLink1" runat="server">
                                    <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                        runat="server" />
                                    质检信息查看</asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
        <table cellpadding="2" cellspacing="1" class="toptable grid" border="1" width="100%">
        <asp:Panel ID="panel3" runat="server">
        <tr style="height:65px">
        <td>
              质检修改内容：
              </td>
              <td colspan="3">
              <asp:TextBox ID="txtREASON" runat="server" TextMode="MultiLine" Height="60px" Width="60%"  ></asp:TextBox>
              </td>
        </tr>
           <tr>
              <td>
              申请人：
              </td>
              <td><asp:TextBox ID="txtapplicant" runat="server"></asp:TextBox>
             
              <asp:HiddenField ID="applicantid" runat="server" /></td>
              <td>
              申请时间：
              </td>
              <td><asp:TextBox ID="txtapplytime" runat="server"></asp:TextBox></td>
              
              
           </tr>
           </asp:Panel>
           <asp:Panel ID="panel1" runat="server">
           <tr runat="server" id="tr_psr"><td>指定评审人</td>
 <td  colspan="3">
 <table cellpadding="2" cellspacing="1" class="toptable grid" border="1" width="100%">
   <tr>
  <td style="width:100px">第一审核人：</td>
  <td><asp:CheckBoxList ID="cbl_qfr" runat="server" RepeatColumns="8">
     </asp:CheckBoxList></td></tr><tr>
  <td style="width:100px">第二审核人：</td>
  <td><asp:CheckBoxList ID="cbl_jsfzr" runat="server" RepeatColumns="8">
     </asp:CheckBoxList></td></tr><tr>
  
  </tr>
 </table>
 </td></tr>
           </asp:Panel>
           <asp:Panel ID="panel2" runat="server">
           <asp:Panel ID="YIJI" runat="server">
         <tr>
           <td>一级评审：</td>
           <td colspan="3">
                      <table cellpadding="2" cellspacing="1" class="toptable grid" border="1" width="100%">
                          <tr>
                          <td> <span>意见：</span> </td>
                          <td colspan="3"><asp:RadioButton ID="RadioTY1" runat="server" Text="同意" GroupName="select1" AutoPostBack="true" />
                          <asp:RadioButton ID="RadioJJ1" runat="server" Text="拒绝" GroupName="select1" AutoPostBack="true" /> 
                          </td>
                          </tr>
                          <tr>
                          <td><span>备注：</span> </td>
                          <td colspan="3">
                          <asp:TextBox ID="TextBZ1" runat="server" TextMode="MultiLine" Width="350px" ></asp:TextBox> <br />
                          </td>
                          </tr>
                          <tr>
                          <td><span>审核人：</span></td><td><asp:TextBox ID="TextSHR1" runat="server" Enabled="false"></asp:TextBox></td>
                          <td><span>审核日期：</span></td><td><asp:TextBox ID="TextSHRQ1" runat="server" Enabled="false"></asp:TextBox></td>
                          </tr>
                      </table>
                      </td>
           </tr>
           </asp:Panel>
            <asp:Panel ID="ERJI" runat="server">
           <tr>
           <td>二级评审：</td>
           
          <td colspan="3">
                      <table cellpadding="2" cellspacing="1" class="toptable grid" border="1" width="100%">
                          <tr>
                          <td> <span>意见：</span> </td>
                          <td colspan="3"><asp:RadioButton ID="RadioTY2" runat="server" Text="同意" GroupName="select2" AutoPostBack="true" />
                                          <asp:RadioButton ID="RadioJJ2" runat="server" Text="拒绝" GroupName="select2" AutoPostBack="true" /> 
                          </td>
                          </tr>
                          <tr>
                          <td><span>备注：</span> </td>
                          <td colspan="3">
                          <asp:TextBox ID="TextBZ2" runat="server" TextMode="MultiLine" Width="350px" ></asp:TextBox> <br />
                          </td>
                          </tr>
                          <tr>
                          <td><span>审核人：</span></td><td><asp:TextBox ID="TextSHR2" runat="server" Enabled="false"></asp:TextBox></td>
                          <td><span>审核日期：</span></td><td><asp:TextBox ID="TextSHRQ2" runat="server" Enabled="false"></asp:TextBox></td>
                          </tr>
                      </table>
                      </td>
                  </tr>
                  </asp:Panel>
                  <%--<asp:Panel ID="SANJI" runat="server">
                   <tr>
           <td>三级评审：</td>
           <td colspan="5">
                      <table cellpadding="2" cellspacing="1" class="toptable grid" border="1" width="100%">
                          <tr>
                          <td><span>意见：</span></td>
                          <td colspan="3">
                          <asp:RadioButton ID="RadioTY3" runat="server" Text="同意" GroupName="select3" AutoPostBack="true" />
                          <asp:RadioButton ID="RadioJJ3" runat="server" Text="拒绝" GroupName="select3" AutoPostBack="true" />
                          </td>
                          </tr>
                          <tr>
                          <td> <span>备注：</span> </td>
                          <td colspan="3"><asp:TextBox ID="TextBZ3" runat="server" TextMode="MultiLine" Width="350px"></asp:TextBox> </td>
                          </tr>
                          <tr>
                          <td> <span>审核人：</span></td><td> <asp:TextBox ID="TextSHR3" runat="server" Enabled="false"></asp:TextBox></td>
                          <td> <span>审核日期：</span></td><td> <asp:TextBox ID="TextSHRQ3" runat="server" Enabled="false"></asp:TextBox></td>
                          </tr>                      
                      </table>
                      </td>
                  </tr>
                  </asp:Panel>--%>
                  </asp:Panel>
           
        </table>
        </div>
    </div>
</asp:Content>
