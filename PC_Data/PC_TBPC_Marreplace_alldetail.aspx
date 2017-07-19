<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Marreplace_alldetail.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Marreplace_alldetail"
    Title="物料代用管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="commanrepeater.css" rel="stylesheet" />

    <script language="javascript" type="text/javascript">
       
    function String.prototype.Trim()   
    {
         return this.replace(/\s+/g,"");
    }  
    //用来计算总数
    function setto(no1,no2)
    {
        var table=document.getElementById("tab"); 
        var tr=table.getElementsByTagName("tr"); 
        var pattem=/^\d+(\.\d+)?$/; 
        var a1;
        var a2;
        var b1;
        var b2;
        var sum1=0;
        var sum2=0;
        var s, ss;
           
                
       for( i=1;i <tr.length-2;i++) 
       {
            b1=parseFloat(tr[i].getElementsByTagName("td")[no1-1].getElementsByTagName("span")[0].innerHTML);//库存数量
            if(pattem.test(tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value.Trim()))
               { 
                    tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value.Trim();
                    a1=parseFloat(tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value); 
                    if(a1<0)
                    {
                        tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value=-tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value;
                        a1=-tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value;
                    }
                    if(b1<a1)
                    {
                        tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value=b1;
                        a1=b1;
                    }
                }
                else 
                {
                    a1=0; 
                    tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value="0";
                }
            sum1+=a1;
        }
        tr[tr.length-2].getElementsByTagName("td")[no2].getElementsByTagName("input")[0].value=sum1.toFixed(4);          
    }
    
</script>
 <style type="text/css"> 
     .autocomplete_completionListElement 
     {  
     	margin : 0px; 
     	background-color : #1C86EE; 
     	color : windowtext; 
     	cursor : 'default'; 
     	text-align : left; 
     	list-style:none; 
     	padding:0px;
        border: solid 1px gray; 
        width:400px!important;   
     }
     .autocomplete_listItem 
     {   
     	border-style : solid; 
     	border :#FFEFDB; 
     	border-width : 1px;  
     	background-color : #EEDC82; 
     	color : windowtext;  
     } 
     .autocomplete_highlightedListItem 
     { 
     	background-color: #1C86EE; 
     	color: black; 
     	padding: 1px; 
     } 
  </style> 
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="btn_cancel" runat="server" Text="取消" OnClick="btn_cancel_click" />&nbsp;
                                <asp:Button ID="btn_confirm" runat="server" Text="确定" OnClick="btn_confirm_click" />&nbsp;
                                <asp:Button ID="btn_addrow" runat="server" Text="增加" OnClick="btn_addrow_Click" />
                                &nbsp;
                                <asp:Button ID="btn_delectrow" runat="server" Text="删除" OnClick="btn_delectrow_Click" />
                                &nbsp;
                                <asp:Button ID="btn_back" runat="server" Text="返回" OnClick="btn_back_click" />&nbsp;
                                <asp:RadioButton ID="rad_summess" runat="server" GroupName="messmod" Text="汇总信息"
                                    TextAlign="Right" Checked="true" AutoPostBack="true" OnCheckedChanged="rad_summess_OnCheckedChanged" />
                                <asp:RadioButton ID="rad_detail" runat="server" GroupName="messmod" Text="详细信息" TextAlign="Right"
                                    AutoPostBack="true" OnCheckedChanged="rad_detail_OnCheckedChanged" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="box-outer">
                        <div>
                            <table width="100%">
                                <tr>
                                    <td>
                                        物料编码:<asp:TextBox ID="tb_marid" runat="server" Text="" AutoPostBack="true" OnTextChanged="tb_marid_Textchanged" title="请输入物料助记码"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="tb_marid" FirstRowSelected="true"
                                            ServicePath="PC_Data_AutoComplete.asmx"  CompletionSetCount="10" MinimumPrefixLength="1" CompletionInterval="10"
                                            ServiceMethod="GetCompletemarbyco1"  CompletionListCssClass="autocomplete_completionListElement"
                                         CompletionListItemCssClass="autocomplete_listItem"  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td>
                                        物料名称:<asp:TextBox ID="tb_marnm" runat="server" Text=""  Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        规格:<asp:TextBox ID="tb_margg" runat="server" Text=""  Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        材质:<asp:TextBox ID="tb_marcz" runat="server" Text=""  Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        国标:<asp:TextBox ID="tb_margb" runat="server" Text=""  Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_lookup" runat="server" Text="查找" OnClick="btn_lookup_click" />&nbsp;
                                        <asp:Button ID="Butbtn_clear" runat="server" Text="清除" onclick="Butbtn_clear_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="overflow: scroll; width: 100%; height: 100%">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td style="font-size: small; text-align: center;" colspan="4">
                                                    被代用物料信息
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" class="nowrap cptable fullwidth">
                                            <tr align="center" class="tableTitle" style="background-color:#5CACEE">
                                                <td id="th00" runat="server" visible="false">
                                                    <strong>代用单号</strong>
                                                </td>
                                                <td id="th01" runat="server" visible="false">
                                                    <strong>工程</strong>
                                                </td>
                                                <td>
                                                    <strong>计划跟踪号</strong>
                                                </td>
                                                <td>
                                                    <strong>原材料编码</strong>
                                                </td>
                                                <td>
                                                    <strong>原材料名称</strong>
                                                </td>
                                                <td>
                                                    <strong>原材料规格</strong>
                                                </td>
                                                <td>
                                                    <strong>原材料材质</strong>
                                                </td>
                                                <td>
                                                    <strong>原材料国标</strong>
                                                </td>
                                                 <td>
                                                    <strong>原材料<br />
                                                        数量</strong>
                                                </td>
                                                <td>
                                                    <strong>原材料<br />
                                                        单位</strong>
                                                </td>
                                                <td runat="server" visible="false">
                                                    <strong>原材料<br />
                                                        辅助数量</strong>
                                                </td>
                                                <td runat="server" visible="false">
                                                    <strong>长度</strong>
                                                </td>
                                                <td runat="server" visible="false">
                                                    <strong>宽度</strong>
                                                </td>
                                                <td runat="server" visible="false">
                                                    <strong>辅助<br />
                                                        单位</strong>
                                                </td>
                                                <td>
                                                    <strong>状态</strong>
                                                </td>
                                            </tr>
                                            <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                <td id="td00" runat="server" visible="false">
                                                    &nbsp;
                                                    <asp:Label ID="PNO" runat="server" Text=""></asp:Label>&nbsp;
                                                </td>
                                                <td id="td01" runat="server" visible="false">
                                                    &nbsp;
                                                    <asp:Label ID="ENGID" runat="server" Text=""></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="YPTCODE" runat="server" Text=""></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="YMARID" runat="server" Text=""></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="YMARNAME" runat="server" Text=""></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="YGUIGE" runat="server" Text=""></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="YCAIZHI" runat="server" Text=""></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="YGUOBIAO" runat="server" Text=""></asp:Label>&nbsp;
                                                </td>
                                             
                                                <td>
                                                    <asp:Label ID="YNUM" runat="server" Text=""></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="YNUNIT" runat="server" Text=""></asp:Label>&nbsp;
                                                </td>
                                                <td runat="server" visible="false">
                                                    <asp:Label ID="YFZNUM" runat="server" Text=""></asp:Label>&nbsp;
                                                </td>
                                                <td runat="server" visible="false">
                                                    <asp:Label ID="YLENGTH" runat="server" Text=""></asp:Label>&nbsp;
                                                </td>
                                                <td runat="server" visible="false">
                                                    <asp:Label ID="YWIDTH" runat="server" Text=""></asp:Label>&nbsp;
                                                </td>
                                                <td runat="server" visible="false">
                                                    <asp:Label ID="YFZUNIT" runat="server" Text=""></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="YSTATE" runat="server" Text="" Visible="false"></asp:Label>&nbsp;
                                                    <asp:Label ID="YSTATETEXT" runat="server" Text=""></asp:Label>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="9" align="center">
                                                    <asp:Panel ID="NoDataPane1" runat="server" Visible="false">
                                                        没有数据！</asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td style="font-size: small; text-align: center;" colspan="4">
                                                    代用物料信息
                                                </td>
                                            </tr>
                                        </table>
                                        <table id="tab" class="nowrap cptable fullwidth" align="center">
                                            <asp:Repeater ID="tbpc_puralldydetailRepeater" runat="server" OnItemDataBound="tbpc_puralldydetailRepeater_ItemDataBound">
                                                <HeaderTemplate>
                                                    <tr align="center" class="tableTitle" style="background-color:#5CACEE">
                                                        <td>
                                                            <strong>行号</strong>
                                                        </td>
                                                        <td>
                                                            <strong>库存唯一标识码</strong>
                                                        </td>
                                                        <td>
                                                            <strong>新材料编码</strong>
                                                        </td>
                                                        <td>
                                                            <strong>新材料名称</strong>
                                                        </td>
                                                        <td>
                                                            <strong>新材料规格</strong>
                                                        </td>
                                                        <td>
                                                            <strong>新材料材质</strong>
                                                        </td>
                                                        <td>
                                                            <strong>新材料国标</strong>
                                                        </td>
                                                        <td>
                                                            <strong>新材料单位</strong>
                                                        </td>
                                                        <td>
                                                            <strong>库存材料量</strong>
                                                        </td>
                                                        
                                                         <td>
                                                            <strong>使用<br />
                                                                数量</strong>
                                                        </td>
                                                        <%--<td id="Td5" runat="server" visible="false">
                                                            <strong>辅助<br />
                                                                数量</strong>
                                                        </td>--%>
                                                        <td runat="server" visible="false">
                                                            <strong>长度</strong>
                                                        </td>
                                                        <td runat="server" visible="false">
                                                            <strong>宽度</strong>
                                                        </td>
                                                        <td id="td0" runat="server" visible="false">
                                                            <strong>库存唯一标识码</strong>
                                                        </td>
                                                        <td id="td1" runat="server" visible="false">
                                                            <strong>入库批号</strong>
                                                        </td>
                                                        <td id="td2" runat="server" visible="false">
                                                            <strong>仓库</strong>
                                                        </td>
                                                        <td id="td3" runat="server" visible="false">
                                                            <strong>仓位</strong>
                                                        </td>
                                                        <td>
                                                            <strong>状态</strong>
                                                        </td>
                                                    </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                        <td>
                                                            <asp:Label ID="Lab_NUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                            <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                                Checked="false"></asp:CheckBox>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="OLDSQCODE" runat="server" Text='<%#Eval("OLDSQCODE")%>'></asp:Label>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="XMARID" runat="server" Text='<%#Eval("XMARID")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="XMARNAME" runat="server" Text='<%#Eval("XMARNAME")%>'></asp:Label>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="XGUIGE" runat="server" Text='<%#Eval("XGUIGE")%>'></asp:Label>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="XCAIZHI" runat="server" Text='<%#Eval("XCAIZHI")%>'></asp:Label>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="XGUOBIAO" runat="server" Text='<%#Eval("XGUOBIAO")%>'></asp:Label>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="XNUNIT" runat="server" Text='<%#Eval("XNUNIT")%>'></asp:Label>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="KUNUM" runat="server" Text='<%#Eval("KUNUM")%>'></asp:Label>&nbsp;
                                                        </td>
                                                       <%-- <td runat="server" visible="false">
                                                            <asp:Label ID="KUFZNUM" runat="server" Text='<%#Eval("KUFZNUM")%>'></asp:Label>&nbsp;
                                                        </td>--%>
                                                         <td>
                                                            <asp:Label ID="LXUSENUM" runat="server" Text='<%#Eval("XUSENUM")%>'></asp:Label>
                                                            <asp:TextBox ID="XUSENUM" runat="server" Text='<%#Eval("XUSENUM")%>' onclick="javascript:if (this.value==''){ this.value='0.00';}else{this.select();}"
                                                                onblur="setto(9,2)"></asp:TextBox>
                                                            &nbsp;
                                                        </td>
                                                         <%--<td id="Td4" runat="server" visible="false">
                                                            <asp:Label ID="LXUSEFZNUM" runat="server" Text='<%#Eval("XUSEFZNUM")%>'></asp:Label>
                                                            <asp:TextBox ID="XUSEFZNUM" runat="server" Text='<%#Eval("XUSEFZNUM")%>' onclick="javascript:if (this.value==''){ this.value='0.00';}else{this.select();}"
                                                                onblur="setto(10,3)"></asp:TextBox>
                                                            &nbsp;
                                                        </td>--%>
                                                        <td runat="server" visible="false">
                                                            <asp:Label ID="LENGTH" runat="server" Text='<%#Eval("LENGTH")%>'></asp:Label>&nbsp;
                                                        </td>
                                                        <td runat="server" visible="false">
                                                            <asp:Label ID="WIDTH" runat="server" Text='<%#Eval("WIDTH")%>'></asp:Label>&nbsp;
                                                        </td>
                                                       
                                                       
                                                        <td id="td0" runat="server" visible="false">
                                                            <asp:Label ID="NEWSQCODE" runat="server" Text='<%#Eval("NEWSQCODE")%>'></asp:Label>&nbsp;
                                                        </td>
                                                        <td id="td1" runat="server" visible="false">
                                                            <asp:Label ID="LOTNUM" runat="server" Text='<%#Eval("LOTNUM")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td id="td2" runat="server" visible="false">
                                                            <asp:Label ID="WAREHOUSEID" runat="server" Text='<%#Eval("WAREHOUSEID")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td id="td3" runat="server" visible="false">
                                                            <asp:Label ID="POSITIONID" runat="server" Text='<%#Eval("POSITIONID")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="STATETEXT" runat="server" Text='<%#get_alldet_state(Eval("STATE").ToString())%>'></asp:Label>
                                                            <asp:Label ID="STATE" runat="server" Text='<%#Eval("STATE")%>' Visible="false"></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <tr align="center">
                                                        <td rowspan="1" colspan="8">
                                                            合计:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tb_kntotal" runat="server" BorderStyle="None" Enabled="False" BackColor="#EFF3FB"></asp:TextBox>
                                                        </td>
                                                        <%--<td runat="server" visible="false">
                                                            <asp:TextBox ID="tb_kfzntotal" runat="server" BorderStyle="None" Enabled="False"
                                                                BackColor="#EFF3FB"></asp:TextBox>
                                                        </td>--%>
                                                         <td>
                                                            <asp:TextBox ID="tb_kuntotal" runat="server" BorderStyle="None" BackColor="#EFF3FB"></asp:TextBox>
                                                        </td>
                                                        <%--<td runat="server" visible="false">
                                                            <asp:TextBox ID="tb_kufzntotal" runat="server" BorderStyle="None" BackColor="#EFF3FB"></asp:TextBox>
                                                        </td>--%>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                            <tr>
                                                <td colspan="11" align="center">
                                                    <asp:Panel ID="NoDataPane2" runat="server" Visible="false">
                                                        没有数据！</asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                
                                
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td style="font-size: small; text-align: center;" colspan="4">
                                                    库存物料信息
                                                </td>
                                            </tr>
                                        </table>
                                        <table id="Table1" class="nowrap cptable fullwidth" align="center">
                                            <asp:Repeater ID="tbpc_marrepkumrepeater" runat="server" OnItemDataBound="tbpc_marrepkumrepeater_ItemDataBound">
                                                <HeaderTemplate>
                                                    <tr align="center" class="tableTitle">
                                                        <td>
                                                            <strong>行号</strong>
                                                        </td>
                                                        <td>
                                                            <strong>材料编码</strong>
                                                        </td>
                                                        <td>
                                                            <strong>材料名称</strong>
                                                        </td>
                                                        <td>
                                                            <strong>材料规格</strong>
                                                        </td>
                                                        <td>
                                                            <strong>材料材质</strong>
                                                        </td>
                                                        <td>
                                                            <strong>材料国标</strong>
                                                        </td>
                                                        <td>
                                                            <strong>材料单位</strong>
                                                        </td>
                                                        <td>
                                                            <strong>库存材料量</strong>
                                                        </td>
                                                        <td>
                                                            <strong>辅助数量</strong>
                                                        </td>
                                                        <td runat="server" visible="false">
                                                            <strong>长度</strong>
                                                        </td>
                                                        <td runat="server" visible="false">
                                                            <strong>宽度</strong>
                                                        </td>
                                                        <td>
                                                            <strong>辅助单位</strong>
                                                        </td>
                                                        <td>
                                                            <strong>库存唯一标识码</strong>
                                                        </td>
                                                    </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                        <td>
                                                            <asp:Label ID="Lab_NUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                            <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                                Checked="false"></asp:CheckBox>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="MARID" runat="server" Text='<%#Eval("MARID")%>'></asp:Label>&nbsp;
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="MARNAME" runat="server" Text='<%#Eval("MARNAME")%>'></asp:Label>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="GUIGE" runat="server" Text='<%#Eval("GUIGE")%>'></asp:Label>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="CAIZHI" runat="server" Text='<%#Eval("CAIZHI")%>'></asp:Label>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="GUOBIAO" runat="server" Text='<%#Eval("GUOBIAO")%>'></asp:Label>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="NUNIT" runat="server" Text='<%#Eval("NUNIT")%>'></asp:Label>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="NUM" runat="server" Text='<%#Eval("NUM")%>'></asp:Label>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="FZNUM" runat="server" Text='<%#Eval("FZNUM")%>'></asp:Label>&nbsp;
                                                        </td>
                                                        <td runat="server" visible="false">
                                                            <asp:Label ID="LENGTH" runat="server" Text='<%#Eval("LENGTH")%>'></asp:Label>&nbsp;
                                                        </td>
                                                        <td runat="server" visible="false">
                                                            <asp:Label ID="WIDTH" runat="server" Text='<%#Eval("WIDTH")%>'></asp:Label>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="FZUNIT" runat="server" Text='<%#Eval("FZUNIT")%>'></asp:Label>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="SQCODE" runat="server" Text='<%#Eval("SQCODE")%>'></asp:Label>&nbsp;
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <tr align="center">
                                                        <td rowspan="1" colspan="7">
                                                            合计:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tb_kuntotal" runat="server" BorderStyle="None" Enabled="False" BackColor="#EFF3FB"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tb_kufzntotal" runat="server" BorderStyle="None" Enabled="False"
                                                                BackColor="#EFF3FB"></asp:TextBox>
                                                        </td>
                                                        <td colspan="2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                            <tr>
                                                <td colspan="13" align="center">
                                                    <asp:Panel ID="NoDataPane3" runat="server" Visible="false">
                                                        没有数据！</asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
