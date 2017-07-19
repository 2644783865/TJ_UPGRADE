<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Masters/RightCotentMasterSession.Master"  CodeBehind="TM_MSOrgInputMode.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MSOrgInputMode" %>

<%@ Register src="../Controls/JSRegister.ascx" tagname="JSRegister" tagprefix="JSR" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    <asp:Label ID="labprostru" runat="server" Text=""></asp:Label></asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link href="../Assets/AutoCompleteTextBox.css" rel="stylesheet" type="text/css" /> 
    <JSR:JSRegister ID="JSRegister1" runat="server" />
    <script src="../JS/SQL.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/TM_MsOrgInputMode.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/ChoiceRcmd.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/TM_ClearSelectedColumns.js" type="text/javascript" charset="GB2312"></script>
     <script src="../JS/TM_ShowInput.js" type="text/javascript" charset="GB2312"></script>    
    <script language="javascript" type="text/javascript">
        function ShowOrgInputed()
        {
           var date=new Date();
           var time=date.getTime();
           var tt=window.showModalDialog("TM_OrgDataInput.aspx?action=<%=tsaid.Text %>&NoUse="+time,'',"dialogHeight:"+screen.availHeight+";dialogWidth:"+screen.availWidth+";status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
//////           if(tt=="Refesh")//不刷新，可能页面上有未保存的数据
//////           {
//////              window.location.reload();
//////           }
        }
        
        function ShowLastFive()
        {
           var date=new Date();
           var time=date.getTime();
           window.open("TM_OrgLastOne.aspx?action=<%=tsaid.Text %>&NoUse="+time);
           //var tt=window.showModalDialog("TM_OrgLastOne.aspx?action=<%=tsaid.Text %>&NoUse="+time,'',"dialogHeight:"+screen.availHeight+";dialogWidth:"+screen.availWidth+";status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
           //if(tt=="Refesh")
           //{
              //window.location.reload();
           //}
        }
        
        function ShowProTip()
        {
           window.showModalDialog("TM_Pro_Stru_List_Tip.aspx",'',"dialogHeight:400px;dialogWidth:950px;status:no;scroll:no;center:yes;toolbar=no;menubar=no");
        }
        
        function getClientId()        {            var paraId1 = '<%= GridView1.ClientID %>';//注册控件1            var paraId2='<%= txtid.ClientID %>';            var paraId3='<%= istid.ClientID %>';            var paraId4='<%= lblNumber.ClientID %>';            var paraId5='<%= txtBxishu.ClientID %>';            var paraId6='<%= txtXxishu.ClientID %>';            return {Id1:paraId1,Id2:paraId2,Id3:paraId3,Id4:paraId4,BXishu:paraId5,XXishu:paraId6};//生成访问器        }
        
        function ShowSetting()
        {
           var obj_showDiv=document.getElementById("clear_div");
           obj_showDiv.className = "show";
           ClearColumns(3);
        }
        
        function CloseDiv()
        {
           var obj_showDiv=document.getElementById("clear_div");
           obj_showDiv.className = "hidden";
        }
    </script>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table width="96%">
                            <tr>
                                <td style="width: 24%">
                                    生产制号:<asp:Label ID="tsaid" runat="server"></asp:Label>
                                    <input id="eng_type" type="text" readonly="readonly" runat="server" style="display: none" />
                                </td>
                                <td style="width: 24%">
                                    项目名称:<asp:Label ID="lab_proname" runat="server"></asp:Label>
                                    <input id="pro_id" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                                </td>
                                <td style="width: 24%">
                                    工程名称:<asp:Label ID="lab_engname" runat="server"></asp:Label>
                                </td>
                                <td>台数:<asp:TextBox ID="lblNumber" runat="server" Width="40px"></asp:TextBox></td>
                                
                                <td align="center">
                                    <asp:HyperLink ID="hpView" CssClass="link" Target="_blank" runat="server">
                                        查 看
                                        <asp:Image ID="ImageTo" ImageUrl="~/Assets/icon-fuction/139.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" /></asp:HyperLink>
                                </td>
                                <td  align="center">
                                <asp:HyperLink ID="hpGoback" CssClass="hand" Target="_blank" onclick="history.go(-1);" runat="server">
                                        返 回
                                        <asp:Image ID="ImageBack"   Height="16" Width="16" ImageUrl="~/Assets/icons/back.png" border="0" hspace="2"
                                            align="absmiddle" runat="server" /></asp:HyperLink></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div>
                <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid">
                    <tr>
                        <td align="left">
                        &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btninsert" runat="server" ToolTip="插入选择行下方,不超过15条！！！" Width="40" Text="插入" OnClientClick="return insert()"
                                OnClick="btninsert_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btndelete" runat="server" Width="40"  Text="删除" OnClientClick="return check()"
                                OnClick="btndelete_Click" />
                        </td>
                        <td>
                        <asp:HyperLink ID="HyperLink4" CssClass="hand" runat="server"><asp:Image ID="Image3" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" align="absmiddle" runat="server" />快捷操作</asp:HyperLink>
                        <input id="lblshortcut" runat="server" readonly="readonly" style="color:Red; background-color:Transparent; border-style:none; width:100px;" value="(当前:序)" type="text" />
                        <asp:HyperLink ID="HyperLink5" CssClass="hand" runat="server" onclick="ShowSetting();"><asp:Image ID="Image4" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" align="absmiddle" runat="server" />设置...</asp:HyperLink>
                            <cc1:HoverMenuExtender ID="HoverMenuExtender6" TargetControlID="HyperLink4" OffsetX="0" OffsetY="20" PopupControlID="Panel6" runat="server">
                            </cc1:HoverMenuExtender>
                            <asp:Panel ID="Panel6" runat="server" style="display:none;" Width="200">
                                <table style='background-color:#f3f3f3; border: #B9D3EE 3px solid;font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>
                                 <tr>
                                  <td>自动添加图号:<input id="ckbTuhao" type="checkbox" runat="server" onclick="Fast_Op(this);" title="勾选后,将鼠标放置到要复制的单元格中,按“↓”键复制至下一单元格" /></td>
                                 </tr>
                                  <tr>
                                   <td>连接前缀图号:<input id="ckbJZTuhao" type="checkbox" runat="server" onclick="Fast_Op(this);" /></td>
                                 </tr>
                                 <tr>
                                  <td>自动添加序号:<input id="ckbXuhao" type="checkbox" runat="server" onclick="Fast_Op(this);" checked="checked" /></td>
                                 </tr>
                                 <tr>
                                   <td>连接前缀序号:<input id="ckbJZXuhao" type="checkbox" runat="server" onclick="Fast_Op(this);" /></td>
                                 </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td>
                            <input id="btnImprotTip" type="button" onclick="ShowProTip();" style="color:Red;width:60px" value="重要提示" /></td>
                        <td align="center">
                            <asp:Button ID="btnsave" runat="server" Text="保存"  Width="40"  OnClientClick="return BomInputCheck();"
                                OnClick="btnsave_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;<input id="btnCheck" type="button" visible="false" style="display:none;" onclick="alert('此功能正在开发！！！');" value="数据检查" />
                        </td>
                        <td>
                          <input id="btnPreOne" runat="server" type="button" onclick="ShowLastFive();" style="width:60px;" value="最近输入" />
                        </td>
                        <td>
                            <input id="btnOrgInputed" runat="server" type="button" onclick="ShowOrgInputed();"
                                value="已输入" />
                        </td>
                        <td>
                            
                        </td>
                        <td>
                            <asp:HyperLink ID="hkoutside" runat="server" Target="_blank" ><asp:Image ID="AddImage1" ImageUrl="~/Assets/images/create.gif" border="0" hspace="0" align="absmiddle" runat="server" />
                             外购加工件
                            </asp:HyperLink>
                        </td>                      
               </tr>
               </table>
            </div>
            <div id="clear_div" class="hidden" style="position:relative; background-color:#f3f3f3; border: #B9D3EE 3px solid; padding:0px; margin:0px;">
              <table>
               <tr>
                 <td><strong>前缀图号:</strong><input id="txtJZTuhao" style="width:100px;" runat="server" type="text" /></td>
                 <td><strong>前缀序号:</strong><input id="txtJZXuhao" style="width:100px;" runat="server" type="text" /></td>
                 <td>
                     &nbsp;&nbsp;&nbsp;<input id="btnClose" type="button" onclick="CloseDiv();" value="关 闭" /></td>
                  <td align="right" style="width:10%;"><strong>待清空列:</strong></td>
                  <td id="td_select" ></td>
                    <td align="right" style="width:10%;"><strong>待清空行:</strong></td>
                    <td>从<input id="txtStartIndex" type="text" onchange="AddControls_ClearSrtRow(this);" style="width:40px;" value="1" />行&nbsp;至&nbsp
                        <input id="txtEndIndex" style="width:40px;" onchange="AddControls_ClearEndRow(this);" type="text" value="1" />行
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;<input id="btnClearUserDefine" type="button"  value="清 空" onclick="return AddControls_Confirm();" /></td>
                    <td>
                        <input id="btnCloseClear" type="button" onclick="Hidden_clear_div();" value="关 闭" />
                    </td>    
               </tr>
               <tr>
                 <td colspan="5"><strong>单重关联:<input id="ckbUnitWght" type="checkbox"  runat="server"/></strong>勾选后，录入数据时【实际单重】=【图纸上单重】</td>
                 <td  align="right"><strong>计划系数:</strong></td>
                 <td colspan="3">(板)<input id="txtBxishu" runat="server" type="text" onblur='var pattem=/^\d+(\.\d+)?$/; if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="1.1";this.foucs();}' style="width:30px"  value="1.1" />(型/圆钢)<input id="txtXxishu" runat="server" style="width:30px"  onblur='var pattem=/^\d+(\.\d+)?$/; if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="1.05";this.foucs();}'  type="text" value="1.05" />(其它)<input id="txtQxishu" runat="server" style="width:30px" disabled="disabled"  type="text" value="1" /></td>
               </tr>
               </table>
            </div>
            <asp:CheckBoxList ID="cklHiddenShow" runat="server" RepeatDirection="Horizontal" RepeatColumns="13" OnSelectedIndexChanged="cklHiddenShow_OnSelectedIndexChanged"  AutoPostBack="true" RepeatLayout="Table">
            </asp:CheckBoxList>
            <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
                CellPadding="2" ForeColor="#333333" Width="100%">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField ItemStyle-Width="10px">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Width="8px" 
                                CssClass="checkBoxCss" />
                        </ItemTemplate>
                        <ItemStyle Width="8px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="行号" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <input id="lblIndex"  onkeyup="grControlFocus(this)" readonly="readonly" style="border-style: none;width: 20px" type="text" value="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="明细序号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="txtMSXuhao" runat="server" style="border-style: none; width: 70px" type="text" onfocus="this.select();"
                                value='<%#Eval("BM_MSXUHAO") %>'  ondblclick="ShowInput(this);"  onkeydown="grControlFocus(this)"/><br />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="txtXuhao" runat="server" style="border-style: none; width: 70px" type="text" onfocus="this.select();"
                                value='<%#Eval("BM_XUHAO") %>'  ondblclick="ShowInput(this);"  onkeydown="grControlFocus(this)"/><br />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="图号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="tuhao" runat="server" style="border-style: none; width: 150px" type="text" onfocus="this.select();" onblur="OrgAutoTuHao(this);"
                                value='<%#Eval("BM_TUHAO") %>'  ondblclick="ShowInput(this);"  onkeydown="grControlFocus(this)"/><br />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="物料编码" >
                        <ItemTemplate>
                            <asp:TextBox ID="marid"  runat="server"  BorderStyle="None" onfocus="this.select();"
                                onchange="autoMsOrgCode(this)" Text='<%# DataBinder.Eval(Container.DataItem, "BM_MARID")%>'
                                Width="90px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="marid_AutoCompleteExtender"   runat="server" CompletionSetCount="15" CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="HmCode" FirstRowSelected="true" 
                                ServicePath="~/Ajax.asmx" TargetControlID="marid" UseContextKey="True" CompletionInterval="10">
                            </cc1:AutoCompleteExtender>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="总序" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <input id="zongxu" runat="server" onchange="OrgAutoXuhao(this)" onmouseover="this.title=this.value;"  ondblclick="ShowInput(this);"  onfocus="autoZongxu(this);this.select();" style="border-style: none;
                                width: 70px" type="text" value='<%#Eval("BM_ZONGXU") %>'   onkeydown="grControlFocus(this)"/><br />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="中文名称">
                        <ItemTemplate>
                            <input id="ch_name" runat="server" style="border-style: none; width: 80px"  ondblclick="ShowInput(this);"  title="请在输入物料编码后再修改此项！！！" onfocus="this.select();"
                                type="text" value='<%#Eval("BM_CHANAME") %>' onkeydown="grControlFocus(this)"/><br />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="体现">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlIsManu" runat="server" onkeydown="grControlFocus(this)"  SelectedValue='<%#Eval("BM_ISMANU") %>' >
                                <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注">
                        <ItemTemplate>
                            <input id="beizhu" runat="server" style="border-style: none; width: 80px" type="text" ondblclick="ShowInput(this);" 
                                value='<%#Eval("BM_NOTE") %>' onkeydown="grControlFocus(this)"/><br />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料长度">
                        <ItemTemplate>
                            <input id="cailiaocd" runat="server" title="mm" onchange="auto(this)" style="border-style: none;
                                width: 50px" type="text" value='<%#Eval("BM_MALENGTH") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料宽度">
                        <ItemTemplate>
                            <input id="cailiaokd" runat="server" title="mm" onchange="auto(this)" style="border-style: none;
                                width: 50px" type="text" value='<%#Eval("BM_MAWIDTH") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单台数量|总数量|计划数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <input id="shuliang" runat="server"  onchange="autoshuliang(this)" class="notbrk"  style="border-style: none;width: 36px" type="text" value='<%#Eval("BM_NUMBER") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                            |&nbsp;<input id="total_shuliang" runat="server" tabindex="-1" class="notbrk" title="单台数量*台数,不能修改" onfocus="this.blur();"  readonly="readonly" value='<%#Eval("BM_TOTALNUMBER") %>' style="border-style: none;width: 36px" type="text" /> 
                            |&nbsp;<input id="plan_shuliang" runat="server" tabindex="-1" onchange="autop_shuliang(this)" class="notbrk" title="材料计划数量" onfocus="this.select();"  style="border-style: none;width: 36px" value='<%#Eval("BM_PNUMBER") %>' type="text" onkeydown="grControlFocus(this)"/>  
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="实际单重" >
                        <ItemTemplate>
                            <input id="dzh" runat="server" onchange="auto1(this);CheckUnitWght_TuUnit(this);" title="kg:制作明细" style="border-style: none; width: 70px"
                                type="text" value='<%#Eval("BM_UNITWGHT") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料单重" >
                        <ItemTemplate>
                            <input id="cailiaodzh" runat="server" title="kg" style="border-style: none; width: 70px" onchange="auto2(this);"
                                type="text" value='<%#Eval("BM_MAUNITWGHT") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料总重">
                        <ItemTemplate>
                            <input id="cailiaozongzhong" title="kg:材料计划" runat="server" onchange="MarTotalWeightChange(this);" style="border-style: none;
                                width: 70px" type="text" value='<%#Eval("BM_MATOTALWGHT") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="面域|计划面域(m2)" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <input id="bgzmy" runat="server" onchange="automy(this)" title="平方米:输入单个数量面域即可，不包含双面！！！" style="border-style: none;
                                width: 60px" type="text" value='<%#Eval("BM_MABGZMY") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                                |&nbsp;<input id="mpmy" runat="server" title="平方米:按【平米】采购计划，在此输入单个面域！！！" style="border-style: none;
                                width: 60px" type="text" value='<%#Eval("BM_MPMY") %>' onfocus="this.select();" onblur='var pattem=/^\d+(\.\d+)?$/; if(this.value!=""){ if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="";this.select();}}'  onkeydown='grControlFocus(this.parentNode.getElementsByTagName("input")[0])'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="条件属性">
                        <ItemTemplate>
                            <input id="tjsx" runat="server" onchange="CheckOrgTJSX(this);"  onfocus="this.select();" style="border-style: none;width: 50px" type="text" value='<%#Eval("BM_BEIZHUATR") %>' onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText="图纸上单重">
                        <ItemTemplate>
                            <input id="tudz" runat="server" title="kg" style="border-style: none; width: 70px" type="text" onchange="TudanZhongBom(this);"
                                value='<%#Eval("BM_TUUNITWGHT") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="图纸上材质">
                        <ItemTemplate>
                            <input id="tucz" runat="server" style="border-style: none; width: 110px" type="text"
                                value='<%#Eval("BM_TUMAQLTY") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="图纸上标准">
                        <ItemTemplate>
                            <input id="tubz" runat="server" style="border-style: none; width: 110px" type="text"
                                value='<%#Eval("BM_TUSTAD") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="图纸上问题">
                        <ItemTemplate>
                            <input id="tuwt" runat="server" style="border-style: none; width: 110px" type="text"
                                value='<%#Eval("BM_TUPROBLEM") %>'  onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材质" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="caizhi" runat="server" readonly="readonly" style="border-style: none;
                                width: 50px" type="text" value='<%#Eval("BM_MAQUALITY") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="规格" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="guige" runat="server"  style="border-style: none; width: 100px" ondblclick="ShowInput(this);" 
                                type="text" value='<%#Eval("BM_GUIGE") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/><br />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料名称" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="cailiaoname" runat="server" readonly="readonly" style="border-style: none;
                                width: 70px" type="text" value='<%#Eval("BM_MANAME") %>' onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料规格" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="cailiaoguige" runat="server" readonly="readonly" style="border-style: none;
                                width: 120px" type="text" value='<%#Eval("BM_MAGUIGE") %>' onkeydown="grControlFocus(this)" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="理论重量" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="lilunzhl" runat="server" readonly="readonly" style="border-style: none;
                                width: 56px" type="text" value='<%#Eval("BM_THRYWGHT") %>' onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="总重" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="zongzhong" runat="server" readonly="readonly" style="border-style: none;
                                width: 70px" type="text" value='<%#Eval("BM_TOTALWGHT") %>' onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料总长" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="cailiaozongchang" runat="server"  style="border-style: none;
                                width: 70px" type="text" value='<%#Eval("BM_MATOTALLGTH") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单位" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="labunit" runat="server" readonly="readonly" title="(技术单位)-(采购单位)" style="border-style: none;
                                width: 32px" type="text" value='<%#Eval("BM_MAUNIT") %>' onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="国标" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="biaozhun" runat="server" readonly="readonly" style="border-style: none;
                                width: 150px" type="text" value='<%#Eval("BM_STANDARD") %>' onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="毛坯" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="xinzhuang" runat="server"  onfocus="this.select();" style="border-style: none; width: 32px" type="text" onchange="ChangeofMarShape(this);"
                                value='<%#Eval("BM_MASHAPE") %>' onkeydown="grControlFocus(this)"/>
                                 <div  class="hidden" style="position:absolute;background-color:#f3f3f3; cursor:hand; border: #B9D3EE 3px solid; padding:0px; margin:0px;">
                                   <ul   style="list-style-type:square;text-align:left; line-height:normal;"></ul>
                                </div>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="zhuangtai" runat="server" onfocus="findChoice(this,1,32);this.select();" style="border-style: none; width: 32px" type="text"
                                value='<%#Eval("BM_MASTATE") %>' onkeydown="grControlFocus(this)"/>
                                   <div  class="hidden" style="position:absolute;background-color:#f3f3f3; cursor:hand; border: #B9D3EE 3px solid; padding:0px; margin:0px;">
                                   <ul   style="list-style-type:square; text-align:left; line-height:normal;"></ul>
                                </div>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="工艺流程">
                        <ItemTemplate>
                            <input id="process" runat="server"  onchange="ChangGY(this);" style="border-style: none; width: 100px" type="text"
                                value='<%#Eval("BM_PROCESS") %>' onkeydown="grControlFocus(this)"/>
                                   <div  class="hidden" style="position:absolute;background-color:#f3f3f3; cursor:hand; border: #B9D3EE 3px solid; padding:0px; margin:0px;">
                                   <ul   style="list-style-type:square; text-align:left; line-height:normal;"></ul>
                                   </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="英文名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                        <ItemTemplate>
                            <input id="en_name" runat="server" style="border-style: none; width: 70px" type="text"
                                value='<%#Eval("BM_ENGSHNAME") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="关键">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlKeyComponents" runat="server" onkeydown="grControlFocus(this)" SelectedValue='<%#Eval("BM_KEYCOMS") %>'>
                                <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="定尺">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlFixedSize" runat="server" onkeydown="grControlFocus(this)" onchange="CtrlFixSize(this);"  SelectedValue='<%#Eval("BM_FIXEDSIZE") %>' >
                                <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="计划">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlWmp" runat="server" onkeydown="grControlFocus(this)" SelectedValue='<%#Eval("BM_WMARPLAN") %>'>
                                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                <asp:ListItem Text="N" Value="N"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="库" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <input id="ku" runat="server" onchange="ChangKu(this);"  onkeydown="grControlFocus(this)" style="border-style: none; width:60px;" type="text"
                                value='<%#Eval("BM_KU") %>' /><br />
                                   <div  class="hidden" style="position:absolute;background-color:#f3f3f3; cursor:hand; border: #B9D3EE 3px solid; padding:0px; margin:0px;">
                                   <ul   style="list-style-type:square; text-align:left; line-height:normal;"></ul>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="部件自重" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="bjzz" runat="server" style="border-style: none;
                                width: 70px" type="text" value='<%#Eval("BM_PARTOWNWGHT") %>' onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                  </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <FixRowColumn TableHeight="450px" TableWidth="100%" />
            </yyc:SmartGridView>
            <div>
                <input id="txtid" type="text" runat="server" readonly="readonly" style="display: none" />
                <input id="add_id" type="text" runat="server" readonly="readonly" style="display: none" />
                <input id="istid" type="text" runat="server" readonly="readonly" style="display: none" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="1">
        <ProgressTemplate>
            <div style="position: absolute; top: 30%; right: 40%">
                <table>
                    <tr>
                        <td align="right">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" />
                        </td>
                        <td align="left" style="background-color: Yellow; font-size: medium;">
                            数据处理中，请稍后...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>

