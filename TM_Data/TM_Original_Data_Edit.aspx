<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_Original_Data_Edit.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Original_Data_Edit" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register src="../Controls/JSRegister.ascx" tagname="JSRegister" tagprefix="JSR" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>原始数据修改</title>
    <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
    <script src="../JS/SQL.js" type="text/javascript" charset="GB2312"></script>
</head>
<body>

<JSR:JSRegister ID="JSRegister1" runat="server" />
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="box-inner">
         <div class="box_right">
             <div class="box-title">
                 <table width="100%">
                    <tr><td> 原始数据(带<span class="red">*</span>号的为必填项)</td></tr>
                 </table>
             </div>
         </div>
     </div>
    <div class="box-wrapper">
        <div class="box-outer">
           <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                <tr>
                <td class="tdleft1">物料编码:</td>     
                <td class="tdright1">
                    <asp:TextBox ID="marid" runat="server" onblur="modifyCode()"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="marid_AutoCompleteExtender" runat="server" 
                        CompletionSetCount="10" DelimiterCharacters="" Enabled="True"
                        MinimumPrefixLength="1" ServiceMethod="HmCode" ServicePath="~/Ajax.asmx" 
                        TargetControlID="marid" UseContextKey="True">
                    </cc1:AutoCompleteExtender>
                    <font color="#ff0000">*</font>
                    <input id="hdmarid" type="text" runat="server" readonly="readonly" style="display: none" />
                </td>
                <td class="tdleft1">材料名称:</td>          
                    <td class="tdright1">
                        <asp:TextBox ID="cailiaoname" runat="server" ></asp:TextBox>
                        <input id="hdcailiaoname" type="text" runat="server" readonly="readonly" style="display: none"/>
                    </td>
               </tr>
                
                <tr>
                 <td class="tdleft1">总序:</td>          
                <td class="tdright1">
                    <asp:TextBox ID="zongxu" runat="server"></asp:TextBox>
                    <input id="hdzongxu" type="text" runat="server" readonly="readonly" style="display: none"/>
                </td>
                <td class="tdleft1">材料规格:</td>          
                <td class="tdright1">
                    <asp:TextBox ID="cailiaoguige" runat="server" ></asp:TextBox>
                    <input id="hdcailiaoguige" type="text" runat="server" readonly="readonly" style="display: none"/>
                </td>
                </tr>
                
                <tr>
                <td class="tdleft1">中文名称:</td>          
                <td class="tdright1">
                    <asp:TextBox ID="cnname" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlKeyComs" runat="server">
                    <asp:ListItem Text="--    --" Value=""></asp:ListItem>
                    <asp:ListItem Text="关键部件" Value="关键部件"></asp:ListItem>
                    </asp:DropDownList>
                    <input id="hdcnname" type="text" runat="server" readonly="readonly" style="display: none"/>
                    <input id="hdkeycoms" type="text" runat="server" readonly="readonly" style="display: none"/>
                </td>
                <td class="tdleft1">英文名称:</td>     
                <td class="tdright1">
                    <asp:TextBox ID="egname" runat="server"></asp:TextBox>
                    <input id="hdegname" type="text" runat="server" readonly="readonly" style="display: none"/>
                </td>
                </tr>
                
                <tr>
                <td class="tdleft1">材料长度:</td>          
                <td class="tdright1">
                    <asp:TextBox ID="cailiaocd" runat="server" onblur="modifyCalculation(1)"></asp:TextBox>
                    <input id="hdcailiaocd" type="text" runat="server" readonly="readonly" style="display: none"/>
                </td>
                <td class="tdleft1">规格:</td>     
                <td class="tdright1">
                    <asp:TextBox ID="guige" runat="server" ></asp:TextBox>
                    <input id="hdguige" type="text" runat="server" readonly="readonly" style="display: none"/>
                </td>      
                </tr>
    
                <tr>
                <td class="tdleft1">材料宽度:</td>          
                    <td class="tdright1">
                        <asp:TextBox ID="cailiaokd" runat="server" onblur="modifyCalculation(2)"></asp:TextBox>
                        <input id="hdcailiaokd" type="text" runat="server" readonly="readonly" style="display: none"/>
                </td>
               <td class="tdleft1">材料总长:</td>          
                <td class="tdright1">
                    <asp:TextBox ID="cailiaozongchang" runat="server"></asp:TextBox>
                    <input id="hdcailiaozongchang" type="text" runat="server" readonly="readonly" style="display: none"/>
                </td> 
                </tr>
                
                 <tr>
                    <td class="tdleft1">数量:</td>          
                    <td class="tdright1">
                        <asp:TextBox ID="shuliang" runat="server" onblur="modifyCalculation(3)"></asp:TextBox>
                        <input id="hdshuliang" type="text" runat="server" readonly="readonly" style="display: none"/>
                    </td>
                    <td class="tdleft1">理论重量:</td>          
                    <td class="tdright1">
                        <asp:TextBox ID="lilunzhl" runat="server" ></asp:TextBox>
                        <input id="hdlilunzhl" type="text" runat="server" readonly="readonly" style="display: none"/>
                    </td>
                 </tr>
                
                <tr>
                    <td class="tdleft1">材料单重:</td>          
                    <td class="tdright1">
                        <asp:TextBox ID="cailiaodzh" runat="server" onblur="Calculation(1)"></asp:TextBox>
                        <input id="hdcailiaodzh" type="text" runat="server" readonly="readonly" style="display: none"/>
                    </td>
                    <td class="tdleft1">单位:</td>     
                    <td class="tdright1">
                        <asp:TextBox id="unit" runat="server" ></asp:TextBox>
                        <input id="hdunit" type="text" runat="server" readonly="readonly" style="display: none"/>
                    </td>
                </tr>
                
                <tr>
                    <td class="tdleft1">面域(m2):</td>          
                    <td class="tdright1">
                        <asp:TextBox ID="bgzmy" runat="server" onblur="Calculation(3)"></asp:TextBox>
                        <input id="hdbgzmy" type="text" runat="server" readonly="readonly" style="display: none"/>
                    </td>
                    <td class="tdleft1">材料总重:</td>          
                    <td class="tdright1">
                        <asp:TextBox ID="cailiaozongzhong" runat="server"></asp:TextBox>
                        <input id="hdcailiaozongzhong" type="text" runat="server" readonly="readonly" style="display: none"/>
                    </td>
                </tr>
                 
                 <tr>
                    <td class="tdleft1">材质:</td>     
                    <td class="tdright1">
                       <asp:TextBox ID="caizhi" runat="server"></asp:TextBox>
                       <input id="hdcaizhi" type="text" runat="server" readonly="readonly" style="display: none" />
                    </td>
                    <td class="tdleft1">毛坯:</td>     
                    <td class="tdright1">
                        <asp:TextBox ID="xinzhuang" runat="server"></asp:TextBox>
                        <input id="hdxinzhuang" type="text" runat="server" readonly="readonly" style="display: none" />
                    </td>
                 </tr>
                 
                 <tr>
                    <td class="tdleft1">单重:</td>     
                    <td class="tdright1">
                        <asp:TextBox ID="dzh" runat="server" onblur="Calculation(2)"></asp:TextBox>
                        <input id="hddzh" type="text" runat="server" readonly="readonly" style="display: none" />
                    </td>
                    <td class="tdleft1">总重:</td>     
                    <td class="tdright1">
                        <asp:TextBox ID="zongzhong" runat="server"></asp:TextBox>
                        <input id="hdzongzhong" type="text" runat="server" readonly="readonly" style="display: none" />
                    </td>
                 </tr>
                 
                 <tr>
                    <td class="tdleft1">标准:</td>     
                    <td class="tdright1">
                        <asp:TextBox id="biaozhun" runat="server"></asp:TextBox>
                        <input id="hdbiaozhun" type="text" runat="server" readonly="readonly" style="display: none" />
                    </td>
                    <td class="tdleft1">状态:</td>     
                    <td class="tdright1">
                        <asp:TextBox id="zhuangtai" runat="server"></asp:TextBox>
                        <input id="hdzhuangtai" type="text" runat="server" readonly="readonly" style="display: none" />
                    </td>
                 </tr>
                 <tr>
                   <td class="tdleft1">是否生成制作明细:</td>
                   <td class="tdright1">
                       <asp:RadioButtonList ID="rblSFZZMX" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                          <asp:ListItem Text="是" Value="1"></asp:ListItem>
                          <asp:ListItem Text="否" Value="0"></asp:ListItem>
                       </asp:RadioButtonList>
                   </td>
                   <td>是否定尺:</td>
                   <td class="tdright1">                       
                       <asp:RadioButtonList ID="rblSFDC" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                          <asp:ListItem Text="是" Value="1"></asp:ListItem>
                          <asp:ListItem Text="否" Value="0"></asp:ListItem>
                       </asp:RadioButtonList>
                       <input id="hdtxtSFDC" type="text" runat="server" readonly="readonly" style="display: none" />
                   </td>
                 </tr>
                 <tr>Z
                   <td class="tdleft1">工艺流程</td>
                   <td class="tdright1" colspan="3">
                       <asp:TextBox ID="txtProcess" runat="server" TextMode="MultiLine" Width="70%"></asp:TextBox>
                       <input id="hdtxtProcess" type="text" runat="server" readonly="readonly" style="display: none" />
                   </td>
                 </tr>
                 <tr>
                    <td class="tdleft1">备注:</td>     
                    <td colspan="3">
                        <asp:TextBox ID="beizhu" runat="server" TextMode="MultiLine" Width="70%"></asp:TextBox>
                        <input id="hdbeizhu" type="text" runat="server" readonly="readonly" style="display: none" />
                    </td>
                 </tr>
                 
                 <tr id="change" runat="server" visible="false">
                    <td class="tdleft1">变更状态:</td>     
                    <td class="tdright1">
                        <asp:RadioButtonList ID="rblstatus" RepeatColumns="3" runat="server"  RepeatDirection="Horizontal"
                            AutoPostBack="true">                
                        <asp:ListItem Text="材料计划" value="0"></asp:ListItem>
                        <asp:ListItem Text="制作明细" Value="1"></asp:ListItem>
                        <asp:ListItem Text="技术外协" Value="2" ></asp:ListItem>
                    </asp:RadioButtonList>
                    <td class="tdleft1">删除:</td> 
                    <td class="tdright1">
                       <asp:LinkButton ID="delete" runat="server" 
                            OnClientClick="return confirm(&quot;确定删除吗？提示：审核通过后删除该条原始数据！&quot;)" Font-Underline="True" 
                            ForeColor="Blue">单条删除</asp:LinkButton> 
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="attChange" runat="server" 
                            OnClientClick="return confirm(&quot;确定删除吗？\r\r提示：审核通过后删除该结构下所有原始数据！&quot;)" Font-Underline="True" 
                            ForeColor="Blue">结构删除</asp:LinkButton>
                    </td>
                    </td>
                 </tr>
            
                <tr>
                    <td class="tdleft1"></td>   
                    <td colspan="3">
                    <asp:Button ID="btnConfirm" runat="server" Text="确 定" onclick="btnConfirm_Click"/>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="取 消" onclick="btnCancel_Click" /> 
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      <asp:Button ID="confirmChange" runat="server" Text="变 更" onclick="confirmChange_Click" />
                    <%--<asp:LinkButton ID="confirmChange" runat="server" Font-Underline="True" 
                            ForeColor="Blue" onclick="confirmChange_Click" >变 更</asp:LinkButton>--%>                    
                    </td>
                 </tr>
                 
            </table>
         </div>
       </div>
       <div>
            <input id="waixie" type="text" runat="server" readonly="readonly" style="display: none" />
            <input id="mpstate" type="text" runat="server" readonly="readonly" style="display: none" />
            <input id="msstate" type="text" runat="server" readonly="readonly" style="display: none" />
       </div>
     </ContentTemplate>
     </asp:UpdatePanel>
    </form>
</body>
</html>
